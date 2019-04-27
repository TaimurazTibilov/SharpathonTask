using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Лицевой (рассчётный) счёт абонента. Может содержать более одного приложения обслуживания</summary>
	[MessagePackObject]
	public class PersonalAccount
	{
		/// <summary>Идентификатор лицевого счёта</summary>
		[Key(0)]
		public string PersonalAccountId { get; set; }

		/// <summary>Контракт, к которому принадлежит данный лицевой счёт</summary>
		[Key(1)]
		public string ContractId { get; set; }

		/// <summary>Метод рассчёта, применяемый к данному счёту</summary>
		[Key(2)]
		public CalculationMethod CalculationMethod { get; set; }

		/// <summary>Идентификатор валюты, в которой производятся рассчёты</summary>
		[Key(3)]
		public string CurrencyCode { get; set; }

		/// <summary>Поставщик услуги (i.e. владлец оборудования, которое обслуживает данныё счёт)</summary>
		[Key(4)]
		public SimpleEntity ServiceProvider { get; set; }
	}
}
