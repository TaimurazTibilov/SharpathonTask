using LinqToDB.Mapping;

namespace SharpathonTask.Data.Model
{
	[Table("tariff_plan")]
	public class TariffPlanModel
	{
		[Column("code")]
		public string Code { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
