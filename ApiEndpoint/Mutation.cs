using System.Collections.Generic;

namespace ApiEndpoint
{
	public class Mutation
	{
		public bool AddService(
			string serviceCode,
			IEnumerable<string> devicesMsisdns)
		{
			return true;
		}
	}
}