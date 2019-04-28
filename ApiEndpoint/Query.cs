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

		public IEnumerable<Contract> GetCustomerContracts(
			int id,
			int page = 0,
			int perPage = 25)
		{
			int counter = 0;
			var contracts = client.GetCustomerContracts(
					new PagedRequest<int> { Key = id }).Items;
			foreach (var contract in contracts)
				if (++counter <= perPage)
					yield return contract;
				else
					yield break;
		}

		public IEnumerable<PersonalAccount> GetCustomerAccounts(
			int id,
			int page = 0,
			int perPage = 25)
		{
			int counter = 0;
			foreach (var contract in GetCustomerContracts(id, page, perPage))
			{
				var accounts = client.GetContractPersonalAccounts(
					new PagedRequest<string> { Key = contract.ContractCode }).Items;
				foreach (var account in accounts)
					if (++counter <= perPage)
						yield return account;
					else
						yield break;
			}
		}

		public IEnumerable<TerminalDevice> GetCustomerDevices(
			int id,
			int page = 0,
			int perPage = 25)
		{
			int counter = 0;
			foreach (var account in GetCustomerAccounts(id, page, perPage))
			{
				var devices = client.GetPersonalAccountTerminalDevices(
					new PagedRequest<string> { Key = account.PersonalAccountId }).Items;
				foreach (var device in devices)
					if (++counter <= perPage)
						yield return device;
					else
						yield break;
			}
		}

		public IEnumerable<PersonalAccount> GetContractAccounts(
			string code,
			int page = 0,
			int perPage = 25)
		{
			var accounts = client.GetContractPersonalAccounts(
				new PagedRequest<string> { Key = code }).Items;
			return accounts.Take(perPage);
		}

		public IEnumerable<TerminalDevice> GetContractDevices(
			string code,
			int page = 0,
			int perPage = 25)
		{
			int counter = 0;
			foreach (var account in GetContractAccounts(code, page, perPage))
			{
				var devices = client.GetPersonalAccountTerminalDevices(
					new PagedRequest<string> { Key = account.PersonalAccountId }).Items;
				foreach (var device in devices)
					if (++counter <= perPage)
						yield return device;
					else
						yield break;
			}
		}

		public IEnumerable<Service> GetAccountServices(
			string code,
			TarificationOption tarification,
			int page = 0,
			int perPage = 25)
		{
			var devices = client.GetPersonalAccountTerminalDevices(
				new PagedRequest<string> { Key = code });

			int counter = 0;
			foreach (var device in devices.Items)
			{
				var services = client.GetTerminalDeviceServices(
					new PagedRequest<string> { Key = device.Msisdn });

				foreach (var service in services.Items)
					if (++counter <= perPage)
						yield return service;
					else
						yield break;
			}
		}
	}
}
