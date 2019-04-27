using SharpathonTask.Contracts;
using SharpathonTask.Data;

namespace SharpathonTask.Server
{
	internal class CustomerData : ICustomerData
	{
		private readonly ICustomerData customerData;

		public CustomerData()
		{
			customerData = new DataAccessor();
		}

		public Customer GetCustomer(PagedRequest<int> customerId)
		{
			return customerData.GetCustomer(customerId);
		}

		public PagedResult<Customer> GetCustomersByNameMask(PagedRequest<string> request)
		{
			return customerData.GetCustomersByNameMask(request);
		}

		public PagedResult<Contract> GetCustomerContracts(PagedRequest<int> request)
		{
			return customerData.GetCustomerContracts(request);
		}

		public PagedResult<PersonalAccount> GetContractPersonalAccounts(PagedRequest<string> request)
		{
			return customerData.GetContractPersonalAccounts(request);
		}

		public PagedResult<TerminalDevice> GetPersonalAccountTerminalDevices(PagedRequest<string> request)
		{
			return customerData.GetPersonalAccountTerminalDevices(request);
		}

		public PagedResult<Service> GetTerminalDeviceServices(PagedRequest<string> request)
		{
			return customerData.GetTerminalDeviceServices(request);
		}
	}
}
