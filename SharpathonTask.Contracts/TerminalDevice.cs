using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Приложение обслуживания, оно же "конечное устройство", идентифицируемое номером телефона (msisdn)</summary>
	[MessagePackObject]
	public class TerminalDevice
	{
		/// <summary>Уникальный идентификатор приложения обслуживания</summary>
		[Key(0)]
		public string Msisdn { get; set; }

		/// <summary>Идентификатор лицевого счёта, к которому принадлежит данное приложение обслуживания</summary>
		[Key(1)]
		public string PersonalAccountCode { get; set; }

		/// <summary>Тарифный план для этого приожения обслуживания</summary>
		[Key(2)]
		public SimpleEntity TariffPlan { get; set; }
	}
}
