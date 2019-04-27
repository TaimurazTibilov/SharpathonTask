using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Mapping;
using SharpathonTask.Contracts;

namespace SharpathonTask.Data.Model
{
	[Table("contract")]
	public class ContractModel
	{
		[Column("id")]
		public string Id { get; set; }

		[Column("customer_id")]
		public int CustomerId { get; set; }

		[Column("legal_category")]
		public LegalCategory LegalCategory { get; set; }

		[Column("marketing_category_code")]
		public string MarketingCategoryCode { get; set; }
	}
}
