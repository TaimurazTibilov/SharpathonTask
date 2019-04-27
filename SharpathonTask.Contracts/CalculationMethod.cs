namespace SharpathonTask.Contracts
{
	/// <summary>Метод рассчёта лицевого счёта</summary>
	public enum CalculationMethod
	{
		/// <summary>Предоплаченный (оплата вносится вперёд использования)</summary>
		PrePaid = 1,

		/// <summary>Авансовый - оплата вносится после использования услуг</summary>
		PostPaid = 2,
	}
}
