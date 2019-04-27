using MessagePack;

namespace SharpathonTask.Contracts
{
	/// <summary>Услуга. Услуги подключаются на приложение обслуживания, но не содержат явной связи с ним, так как не является уникальной для этого ПО</summary>
	[MessagePackObject]
	public class Service
	{
		/// <summary>Уникальный код услуги</summary>
		[Key(0)]
		public string Code { get; set; }

		/// <summary>Имя услуги</summary>
		[Key(1)]
		public string Name { get; set; }
	}
}
