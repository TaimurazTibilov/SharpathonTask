using LinqToDB.Mapping;

namespace SharpathonTask.Data.Model
{
	[Table("service_provider")]
	public class ServiceProviderModel
	{
		[Column("code")]
		public string Code { get; set; }

		[Column("name")]
		public string Name { get; set; }
	}
}
