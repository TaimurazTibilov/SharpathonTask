using System.Collections.Generic;
using MessagePack;

namespace SharpathonTask.Contracts
{
	[MessagePackObject]
	public class PagedResult<T>
	{
		[Key(0)]
		public int PageNumber { get; set; }

		[Key(1)]
		public int TotalPages { get; set; }

		[Key(2)]
		public List<T> Items { get; set; }
	}
}
