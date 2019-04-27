using SharpathonTask.Contracts;

namespace SharpathonTask.Server
{
	internal class CustomerData : ICustomerData
	{
		public Customer GetCustomer(PagedRequest<int> customerId)
		{
			return null;
		}

		public PagedResult<Customer> GetCustomersByNameMask(PagedRequest<string> request)
		{
			return new PagedResult<Customer>();
		}

		public PagedResult<Contract> GetCustomerContracts(PagedRequest<int> request)
		{
			return new PagedResult<Contract>();
		}

		public PagedResult<PersonalAccount> GetContractPersonalAccounts(PagedRequest<string> request)
		{
			return new PagedResult<PersonalAccount>();
		}

		public PagedResult<TerminalDevice> GetPersonalAccountTerminalDevices(PagedRequest<string> request)
		{
			return new PagedResult<TerminalDevice>();
		}

		public PagedResult<Service> GetTerminalDeviceServices(PagedRequest<string> request)
		{
			return new PagedResult<Service>();
		}
	}
}
