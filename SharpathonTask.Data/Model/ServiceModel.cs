using LinqToDB.Mapping;

namespace SharpathonTask.Data
{
	[Table("service")]
	public class ServiceModel
	{
		[Column("code")]
		public string Code { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
