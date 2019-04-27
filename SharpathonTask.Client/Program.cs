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
			var customers = client.GetCustomersByNameMask(new PagedRequest<string>());
			foreach (var customer in customers.Items)
			{
				var contracts = client.GetCustomerContracts(new PagedRequest<int> {Key = customer.CustomerId});
				foreach (var contract in contracts.Items)
				{
					var personalAccounts = client.GetContractPersonalAccounts(new PagedRequest<string> {Key = contract.ContractCode});
					foreach (var personalAccount in personalAccounts.Items)
					{
						var terminalDevices = client.GetPersonalAccountTerminalDevices(new PagedRequest<string> {Key = personalAccount.PersonalAccountId});
						foreach (var terminalDevice in terminalDevices.Items)
						{
							var services = client.GetTerminalDeviceServices(new PagedRequest<string> {Key = terminalDevice.Msisdn});
						}
					}
				}
			}
		}
	}
}
