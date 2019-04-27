using LinqToDB.Mapping;

namespace SharpathonTask.Data.Model
{
	[Table("marketing_category")]
	public class MarketingCategoryModel
	{
		[Column("code")]
		public string Code { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
