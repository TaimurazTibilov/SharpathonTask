using System.Collections.Generic;
using SharpathonTask.Contracts;

namespace SharpathonTask.Server
{
	internal class CustomerData : ICustomerData
	{
		public PagedResult<Service> GetServices(PagedRequest<string> request)
		{
			return new PagedResult<Service>
			{
				TotalPages = 1,
				PageNumber = 0,
				Items = new List<Service>
				{
					new Service
					{
						Code = "CODE",
					},
				},
			};
		}
	}
}
