using LinqToDB.Mapping;

namespace SharpathonTask.Data.Model
{
	[Table("terminal_device_service")]
	public class TerminalDeviceServiceModel
	{
		[Column("msisdn")]
		public string Msisdn { get; set; }

		[Column("service_code")]
		public string ServiceCode { get; set; }
	}
}
