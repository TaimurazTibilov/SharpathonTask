using MessagePack;

namespace SharpathonTask.Contracts
{
	[MessagePackObject]
	public class PagedRequest<T>
	{
		[Key(0)]
		public int ItemsPerPage { get; set; }

		[Key(1)]
		public int PageNumber { get; set; }

		[Key(2)]
		public T Key { get; set; }
	}
}
