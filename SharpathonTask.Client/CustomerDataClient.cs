using System.Runtime.CompilerServices;
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

		public Customer GetCustomer(PagedRequest<int> customerId)
		{
			return Invoke<PagedRequest<int>, Customer>(customerId);
		}

		public PagedResult<Customer> GetCustomersByNameMask(PagedRequest<string> request)
		{
			return Invoke<PagedRequest<string>, PagedResult<Customer>>(request);
		}

		public PagedResult<Contract> GetCustomerContracts(PagedRequest<int> request)
		{
			return Invoke<PagedRequest<int>, PagedResult<Contract>>(request);
		}

		public PagedResult<PersonalAccount> GetContractPersonalAccounts(PagedRequest<string> request)
		{
			return Invoke<PagedRequest<string>, PagedResult<PersonalAccount>>(request);
		}

		public PagedResult<TerminalDevice> GetPersonalAccountTerminalDevices(PagedRequest<string> request)
		{
			return Invoke<PagedRequest<string>, PagedResult<TerminalDevice>>(request);
		}

		public PagedResult<Service> GetTerminalDeviceServices(PagedRequest<string> request)
		{
			return Invoke<PagedRequest<string>, PagedResult<Service>>(request);
		}

		private TResult Invoke<TRequest, TResult>(TRequest request, [CallerMemberName] string callerMethod = null)
			where TRequest : class
			where TResult : class
		{
			var reqMarshaler = new Marshaller<TRequest>(
				MessagePackSerializer.Serialize,
				MessagePackSerializer.Deserialize<TRequest>);
			var resMarshaler = new Marshaller<TResult>(
				MessagePackSerializer.Serialize,
				MessagePackSerializer.Deserialize<TResult>);

			var method = new Method<TRequest, TResult>(
				MethodType.Unary,
				nameof(ICustomerData),
				callerMethod,
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
