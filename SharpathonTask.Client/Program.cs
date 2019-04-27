using Grpc.Core;
using SharpathonTask.Contracts;

namespace SharpathonTask.Client
{
	class Program
	{
		internal static void Main()
		{
			var channel = new Channel("127.0.0.1", 10500, ChannelCredentials.Insecure);
			var client = new CustomerDataClient(channel);
			var result = client.GetServices(new PagedRequest<string>());
		}
	}
}
