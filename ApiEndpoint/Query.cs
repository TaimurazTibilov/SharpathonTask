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

	}

	public class Query
	{
		public TerminalDevice[] GetCustomerDevices(
			int customerId,
			int page,
			int itemsPerPage)
		{
			return new[] { new TerminalDevice { Msisdn = 2, PersonalAccountCode = "234" } };
		}

		public Service[] GetAccountServices(
			string accountCode,
			TarificationOption tarificationOption,
			int page,
			int itemsPerPage)
		{
			return new[] { new Service { Code = "123", Name = "456" } };
		}
	}
}
