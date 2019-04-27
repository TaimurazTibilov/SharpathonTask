using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Простая сущность, идентифицируемая кодом и имеющая имя</summary>
	[MessagePackObject]
	public class SimpleEntity
	{
		/// <summary>Код экземпляра сущности</summary>
		[Key(0)]
		public string Code { get; set; }

		/// <summary>Имя экземпляра сущности</summary>
		[Key(1)]
		public string Name { get; set; }
	}
}
