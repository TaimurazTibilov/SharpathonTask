using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Mapping;
using SharpathonTask.Contracts;

namespace SharpathonTask.Data.Model
{
	[Table("personal_account")]
	public class PersonalAccountModel
	{
		[Column("id")]
		public string Id { get; set; }

		[Column("contract_id")]
		public string ContractId { get; set; }

		[Column("calculation_method")]
		public CalculationMethod CalculationMethod { get; set; }

		[Column("currency_code")]
		public string CurrencyCode { get; set; }

		[Column("service_provider_code")]
		public string ServiceProviderCode { get; set; }
	}
}
