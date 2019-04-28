using Grpc.Core;
using HotChocolate.Types;
using SharpathonTask.Client;
using SharpathonTask.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEndpoint
{
	public enum TarificationOption
	{
		Periodic,
		Counter,
	}

	public class Query
	{
		private static CustomerDataClient client;

		static Query()
		{
			var channel = new Channel("127.0.0.1", 10500, ChannelCredentials.Insecure);
			client = new CustomerDataClient(channel);
		}

		public IEnumerable<TerminalDevice> GetCustomerDevices(
			int customerId,
			int page,
			int itemsPerPage)
		{
			var contracts = client.GetCustomerContracts(
				new PagedRequest<int> { Key = customerId }).Items;
			foreach (var contract in contracts)
			{
				var accounts = client.GetContractPersonalAccounts(
					new PagedRequest<string> { Key = contract.ContractCode }).Items;
				foreach (var account in accounts)
				{
					var devices = client.GetPersonalAccountTerminalDevices(
						new PagedRequest<string> { Key = account.PersonalAccountId }).Items;
					foreach (var device in devices)
						yield return device;
				}
			}
		}

		public IEnumerable<Service> GetAccountServices(
			string accountCode,
			TarificationOption tarificationOption,
			int page,
			int itemsPerPage)
		{
			yield return new Service { Code = "123", Name = "456" };
		}
	}
}
