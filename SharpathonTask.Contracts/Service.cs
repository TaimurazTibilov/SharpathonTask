using MessagePack;

namespace SharpathonTask.Contracts
{
	[MessagePackObject]
	public class Service
	{
		[Key(0)]
		public string Code { get; set; }
	}
}
