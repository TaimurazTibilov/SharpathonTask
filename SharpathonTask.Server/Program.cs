using System;
using System.Threading.Tasks;
using Grpc.Core;
using MessagePack;
using SharpathonTask.Contracts;

namespace SharpathonTask.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			var customerData = new CustomerData();

			var server = new Grpc.Core.Server
			{
				Ports = { { "127.0.0.1", 10500, ServerCredentials.Insecure } },
				Services =
				{
					GetServiceDefinition(customerData),
				}
			};

			server.Start();

			Console.WriteLine("Press any key to shutdown");
			Console.ReadKey(true);
			server.ShutdownAsync().Wait();
			Console.WriteLine();
			Console.WriteLine("Shutdown complete. Press any key to exit");
			Console.ReadKey(true);
		}

		private static ServerServiceDefinition GetServiceDefinition(ICustomerData customerData)
		{
			var builder = ServerServiceDefinition.CreateBuilder();
			AddMethod<PagedRequest<int>, Customer>(builder, nameof(ICustomerData.GetCustomer), customerData.GetCustomer);
			AddMethod<PagedRequest<string>, PagedResult<Customer>>(builder, nameof(ICustomerData.GetCustomersByNameMask), customerData.GetCustomersByNameMask);
			AddMethod<PagedRequest<int>, PagedResult<Contract>>(builder, nameof(ICustomerData.GetCustomerContracts), customerData.GetCustomerContracts);
			AddMethod<PagedRequest<string>, PagedResult<PersonalAccount>>(builder, nameof(ICustomerData.GetContractPersonalAccounts), customerData.GetContractPersonalAccounts);
			AddMethod<PagedRequest<string>, PagedResult<TerminalDevice>>(builder, nameof(ICustomerData.GetPersonalAccountTerminalDevices), customerData.GetPersonalAccountTerminalDevices);
			AddMethod<PagedRequest<string>, PagedResult<Service>>(builder, nameof(ICustomerData.GetTerminalDeviceServices), customerData.GetTerminalDeviceServices);

			return builder.Build();
		}

		private static void AddMethod<TRequest, TResult>(ServerServiceDefinition.Builder builder, string name, Func<TRequest, TResult> handler)
			where TRequest : class
			where TResult : class
		{
			var method = new Method<TRequest, TResult>(
				MethodType.Unary,
				nameof(ICustomerData),
				name,
				new Marshaller<TRequest>(MessagePackSerializer.Serialize, MessagePackSerializer.Deserialize<TRequest>),
				new Marshaller<TResult>(MessagePackSerializer.Serialize, MessagePackSerializer.Deserialize<TResult>));

			builder.AddMethod(method, (request, context) =>
			{
				var result = handler(request);
				return Task.FromResult(result);
			});
		}
	}
}
