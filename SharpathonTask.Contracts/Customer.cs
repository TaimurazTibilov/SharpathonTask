using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Клиент (абонент). На одного абонента может быть заключено более одного контракта</summary>
	[MessagePackObject]
	public class Customer
	{
		/// <summary>Уникальный идентификатор клиента</summary>
		[Key(0)]
		public int CustomerId { get; set; }

		/// <summary>ФИО клиента/название организации</summary>
		[Key(1)]
		public string CustomerName { get; set; }

		/// <summary>Тип клиента</summary>
		[Key(2)]
		public CustomerType CustomerType { get; set; }
	}
}
