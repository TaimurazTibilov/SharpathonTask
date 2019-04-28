using HotChocolate.Types;
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
		public IEnumerable<TerminalDevice> GetCustomerDevices(
			int customerId,
			int page,
			int itemsPerPage)
		{
			yield return new TerminalDevice { Msisdn = "2", PersonalAccountCode = "234" };
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
