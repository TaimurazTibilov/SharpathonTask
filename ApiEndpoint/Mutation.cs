using System.Collections.Generic;

namespace ApiEndpoint
{
	public class Mutation
	{
		public bool AddService(
			string code,
			IEnumerable<string> devicesMsisdns)
		{
			return true;
		}
	}
}