using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Core;
using MessagePack;

namespace SharpathonTask.Contracts
{
	public static class GrpcExtensions
	{
		private static readonly Type MethodType = typeof(Method<,>);
		private static readonly Type MarshallerType = typeof(Marshaller<>);
		private static readonly Type MessagePackSerializerType = typeof(MessagePackSerializer);

		private static readonly MethodInfo MessagePackSerialize = MessagePackSerializerType
			.GetMethods()
			.FirstOrDefault(m =>
				m.Name == nameof(MessagePackSerializer.Serialize)
				&& m.IsGenericMethodDefinition
				&& m.GetParameters().Length == 1
				&& m.ReturnType == typeof(byte[]));
		private static readonly MethodInfo MessagePackDeserialize = MessagePackSerializerType
			.GetMethods()
			.FirstOrDefault(m =>
				m.Name == nameof(MessagePackSerializer.Deserialize)
				&& m.IsGenericMethodDefinition
				&& m.GetParameters().Length == 1
				&& m.GetParameters()[0].ParameterType == typeof(byte[]));

		private static readonly MethodInfo BuilderUnaryAddMethod = typeof(ServerServiceDefinition.Builder)
			.GetMethods()
			.FirstOrDefault(m =>
				m.Name == nameof(ServerServiceDefinition.Builder.AddMethod)
				&& m.IsGenericMethodDefinition
				&& m.GetParameters().Length == 2
				&& m.GetParameters()[1].ParameterType.Name == typeof(UnaryServerMethod<,>).Name);

		public static ServerServiceDefinition.Builder AddMessagePackInterface<T, TImpl>(this ServerServiceDefinition.Builder builder, TImpl implementation)
			where TImpl : class, T
		{
			var name = typeof(T).Name;
			var methods = typeof(T).GetMethods();
			foreach (var method in methods)
			{
				if (method.ReturnType == typeof(void))
					continue;

				var parameters = method.GetParameters();
				if (parameters.Length != 1)
					continue; //not supported by Method<,>, requires wrapping

				var concreteType = MethodType.MakeGenericType(parameters[0].ParameterType, method.ReturnType);
				var inputMarshaller = MarshallerType.MakeGenericType(parameters[0].ParameterType);
				var outputMarshaller = MarshallerType.MakeGenericType(method.ReturnType);

				var inputSerializer = typeof(Func<,>).MakeGenericType(parameters[0].ParameterType, typeof(byte[]));
				var inputDeserializer = typeof(Func<,>).MakeGenericType(typeof(byte[]), parameters[0].ParameterType);
				var outputSerializer = typeof(Func<,>).MakeGenericType(method.ReturnType, typeof(byte[]));
				var outputDeserializer = typeof(Func<,>).MakeGenericType(typeof(byte[]), method.ReturnType);

				var methodConstructor = concreteType.GetConstructor(new[]
				{
					typeof(MethodType),
					typeof(string),
					typeof(string),
					inputMarshaller,
					outputMarshaller,
				});
				if (methodConstructor == null)
					continue;

				var inputMarshallerConstructor = inputMarshaller.GetConstructor(new[]
				{
					inputSerializer,
					inputDeserializer,
				});
				if (inputMarshallerConstructor == null)
					continue;

				var outputMarshallerConstructor = outputMarshaller.GetConstructor(new[]
				{
					outputSerializer,
					outputDeserializer,
				});
				if (outputMarshallerConstructor == null)
					continue;

				var grpcMethod = methodConstructor.Invoke(new[]
				{
					Grpc.Core.MethodType.Unary,
					name,
					method.Name,
					inputMarshallerConstructor.Invoke(new object[]
					{
						Delegate.CreateDelegate(inputSerializer, MessagePackSerialize.MakeGenericMethod(parameters[0].ParameterType)),
						Delegate.CreateDelegate(inputDeserializer, MessagePackDeserialize.MakeGenericMethod(parameters[0].ParameterType)),
					}),
					outputMarshallerConstructor.Invoke(new object[]
					{
						Delegate.CreateDelegate(outputSerializer, MessagePackSerialize.MakeGenericMethod(method.ReturnType)),
						Delegate.CreateDelegate(outputDeserializer, MessagePackDeserialize.MakeGenericMethod(method.ReturnType)),
					}),
				});

				var implMethod = typeof(TImpl).GetMethod(method.Name, new[] {parameters[0].ParameterType});

				var blockReturnType = typeof(Task<>).MakeGenericType(method.ReturnType);
				var taskFromResultMethod = typeof(Task)
					.GetMethods()
					.First(m => m.Name == "FromResult")
					.MakeGenericMethod(method.ReturnType);

				var requestParameter = Expression.Parameter(parameters[0].ParameterType, "request");
				var contextParameter = Expression.Parameter(typeof(ServerCallContext), "context");
				var returnTarget = Expression.Label(blockReturnType);
				var block = Expression.Block(
					blockReturnType,
					new[]
					{
						requestParameter,
						contextParameter,
					},
					Expression.Return(
						returnTarget,
						Expression.Call(
							taskFromResultMethod,
							Expression.Call(
								Expression.Constant(implementation, typeof(TImpl)),
								implMethod,
								requestParameter))),
					Expression.Label(returnTarget, Expression.Constant(null, blockReturnType)));

				var unaryMethodType = typeof(UnaryServerMethod<,>).MakeGenericType(parameters[0].ParameterType, method.ReturnType);
				var lambdaMethod = typeof(Expression)
					.GetMethods()
					.First(m =>
						m.Name == "Lambda"
						&& m.IsGenericMethodDefinition
						&& m.GetParameters().Length == 2
						&& m.GetParameters()[0].ParameterType == typeof(Expression)
						&& m.GetParameters()[1].ParameterType == typeof(ParameterExpression[]));
				var concreteLambdaMethod = lambdaMethod.MakeGenericMethod(unaryMethodType);
				var concreteLambdaType = typeof(Expression<>).MakeGenericType(unaryMethodType);
				var compileMethod = concreteLambdaType.GetMethod("Compile", new Type[0]);

				var concreteExpression = concreteLambdaMethod.Invoke(null, new object[] { block, new[] { requestParameter, contextParameter } });
				var compiledDelegate = compileMethod.Invoke(concreteExpression, new object[0]);


				var add = BuilderUnaryAddMethod.MakeGenericMethod(parameters[0].ParameterType, method.ReturnType);
				builder = (ServerServiceDefinition.Builder)add.Invoke(builder, new[] {grpcMethod, compiledDelegate});
			}
			return builder;
		}
	}
}
