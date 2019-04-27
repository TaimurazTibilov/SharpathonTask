using LinqToDB.Mapping;

namespace SharpathonTask.Data.Model
{
	[Table("terminal_device")]
	public class TerminalDeviceModel
	{
		[Column("msisdn")]
		public string Msisdn { get; set; }

		[Column("personal_account_code")]
		public string PersonalAccountCode { get; set; }

		[Column("tariff_plan_code")]
		public string TariffPlanCode { get; set; }
	}
}
