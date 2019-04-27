using Grpc.Core;
using MessagePack;
using SharpathonTask.Contracts;

namespace SharpathonTask.Client
{
	public class CustomerDataClient : ICustomerData
	{
		private readonly DefaultCallInvoker m_callInvoker;

		public CustomerDataClient(Channel channel)
		{
			m_callInvoker = new DefaultCallInvoker(channel);
		}

		public PagedResult<Service> GetServices(PagedRequest<string> request)
		{
			var reqMarshaler = new Marshaller<PagedRequest<string>>(
				MessagePackSerializer.Serialize,
				MessagePackSerializer.Deserialize<PagedRequest<string>>);
			var resMarshaler = new Marshaller<PagedResult<Service>>(
				MessagePackSerializer.Serialize,
				MessagePackSerializer.Deserialize<PagedResult<Service>>);

			var method = new Method<PagedRequest<string>, PagedResult<Service>>(
				MethodType.Unary,
				nameof(ICustomerData),
				nameof(GetServices),
				reqMarshaler,
				resMarshaler);
			return m_callInvoker.BlockingUnaryCall(
				method,
				null,
				new CallOptions(),
				request);
		}
	}
}
