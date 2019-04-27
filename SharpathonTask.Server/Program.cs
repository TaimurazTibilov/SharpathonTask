using System;
using Grpc.Core;
using SharpathonTask.Contracts;

namespace SharpathonTask.Server
{
	class Program
	{
		static void Main(string[] args)
		{
			var customerData = new CustomerData();

			var server = new Grpc.Core.Server
			{
				Ports = { { "127.0.0.1", 10500, ServerCredentials.Insecure } },
				Services =
				{
					ServerServiceDefinition.CreateBuilder()
						.AddMessagePackInterface<ICustomerData, CustomerData>(customerData)
						.Build()
				}
			};

			server.Start();

			Console.WriteLine("Press any key to shutdown");
			Console.ReadKey(true);
			server.ShutdownAsync().Wait();
			Console.WriteLine();
			Console.WriteLine("Shutdown complete. Press any key to exit");
			Console.ReadKey(true);
		}
	}
}
