using Grpc.Core;
using SharpathonTask.Contracts;

namespace SharpathonTask.Client
{
	class Program
	{
		internal static void Main()
		{
			var channel = new Channel("127.0.0.1", 10500, ChannelCredentials.Insecure);
			var client = new CustomerDataClient(channel);
			var customer = client.GetCustomer(new PagedRequest<int>());
			var customers = client.GetCustomersByNameMask(new PagedRequest<string>());
			client.GetCustomerContracts(new PagedRequest<int>());
			client.GetContractPersonalAccounts(new PagedRequest<string>());
			client.GetPersonalAccountTerminalDevices(new PagedRequest<string>());
			client.GetTerminalDeviceServices(new PagedRequest<string>());
		}
	}
}
