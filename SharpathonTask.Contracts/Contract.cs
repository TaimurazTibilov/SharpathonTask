using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Контракт. Содержит не менее одного лицевого счёта</summary>
	[MessagePackObject]
	public class Contract
	{
		/// <summary>Уникальный идентификатор контракта</summary>
		[Key(0)]
		public string ContractCode { get; set; }

		/// <summary>Идентификатор клиента, на которого заключён данный контракт</summary>
		[Key(1)]
		public int CustomerId { get; set; }

		/// <summary>Маркетинговая категория данного контракта (ценность контракта с точки зрения отдела маркетинга)</summary>
		[Key(2)]
		public SimpleEntity MarketingCategory { get; set; }

		/// <summary>Категория "легальности" контракта</summary>
		[Key(3)]
		public LegalCategory LegalCategory { get; set; }
	}
}
