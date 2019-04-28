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
		private static Random rnd = new Random();

		static Query()
		{
			var channel = new Channel("127.0.0.1", 10500, ChannelCredentials.Insecure);
			client = new CustomerDataClient(channel);
		}

		public IEnumerable<TerminalDevice> GetCustomerDevices(
			int customerId,
			int page = 0,
			int itemsPerPage = 25)
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
			int page = 0,
			int itemsPerPage = 25)
		{
			var devices = client.GetPersonalAccountTerminalDevices(
				new PagedRequest<string> { Key = accountCode });

			foreach (var device in devices.Items)
			{
				var services = client.GetTerminalDeviceServices(
					new PagedRequest<string> { Key = device.Msisdn });

				foreach (var service in services.Items)
					if (rnd.NextDouble() < 0.5)
						yield return service;
			}
		}
	}
}
