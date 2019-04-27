using LinqToDB.Mapping;
using SharpathonTask.Contracts;

namespace SharpathonTask.Data.Model
{
	[Table("customer")]
	public class CustomerModel
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("type")]
		public CustomerType Type { get; set; }
	}
}
