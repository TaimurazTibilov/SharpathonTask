using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.DataProvider.SQLite;
using SharpathonTask.Contracts;
using SharpathonTask.Data.Model;

namespace SharpathonTask.Data
{
	public class DataAccessor : ICustomerData
	{
		public List<ServiceModel> GetServices()
		{
			using (var connection = GetDataConnection())
			{
				return connection.GetTable<ServiceModel>().ToList();
			}
		}

		public Customer GetCustomer(PagedRequest<int> customerId)
		{
			using (var connection = GetDataConnection())
			{
				var customer = connection
					.GetTable<CustomerModel>()
					.FirstOrDefault(cm => cm.Id == customerId.Key);
				if (customer == null)
					return null;
				return new Customer
				{
					CustomerId = customer.Id,
					CustomerName = customer.Name,
					CustomerType = customer.Type,
				};
			}
		}

		public PagedResult<Customer> GetCustomersByNameMask(PagedRequest<string> request)
		{
			var key = request.Key?.ToUpper();
			using (var connection = GetDataConnection())
			{
				var customers = connection
					.GetTable<CustomerModel>()
					.Where(cm => string.IsNullOrEmpty(key) || cm.Name.ToUpper().Contains(key))
					.Select(cm => new Customer
					{
						CustomerId = cm.Id,
						CustomerName = cm.Name,
						CustomerType = cm.Type,
					})
					.ToList();
				return new PagedResult<Customer> {Items = customers};
			}
		}

		public PagedResult<Contract> GetCustomerContracts(PagedRequest<int> request)
		{
			using (var connection = GetDataConnection())
			{
				var contracts = connection.GetTable<ContractModel>();
				var mcs = connection.GetTable<MarketingCategoryModel>();

				var query =
					from cm in contracts
					join mc in mcs on cm.MarketingCategoryCode equals mc.Code
					where cm.CustomerId == request.Key
					select new Contract
					{
						ContractCode = cm.Id,
						CustomerId = cm.CustomerId,
						LegalCategory = cm.LegalCategory,
						MarketingCategory = new SimpleEntity
						{
							Code = mc.Code,
							Name = mc.Name,
						},
					};
				return new PagedResult<Contract>{Items = query.ToList()};
			}
		}

		public PagedResult<PersonalAccount> GetContractPersonalAccounts(PagedRequest<string> request)
		{
			using (var connection = GetDataConnection())
			{
				var pas = connection.GetTable<PersonalAccountModel>();
				var sps = connection.GetTable<ServiceProviderModel>();

				var query =
					from pa in pas
					join sp in sps on pa.ServiceProviderCode equals sp.Code
					where pa.ContractId == request.Key
					select new PersonalAccount
					{
						PersonalAccountId = pa.Id,
						ContractId = pa.ContractId,
						CalculationMethod = pa.CalculationMethod,
						CurrencyCode = pa.CurrencyCode,
						ServiceProvider = new SimpleEntity
						{
							Code = sp.Code,
							Name = sp.Name,
						},
					};
				return new PagedResult<PersonalAccount> { Items = query.ToList() };
			}
		}

		public PagedResult<TerminalDevice> GetPersonalAccountTerminalDevices(PagedRequest<string> request)
		{
			using (var connection = GetDataConnection())
			{
				var tds = connection.GetTable<TerminalDeviceModel>();
				var tps = connection.GetTable<TariffPlanModel>();

				var query =
					from td in tds
					join tp in tps on td.TariffPlanCode equals tp.Code
					where td.PersonalAccountCode == request.Key
					select new TerminalDevice
					{
						Msisdn = td.Msisdn,
						PersonalAccountCode = td.PersonalAccountCode,
						TariffPlan = new SimpleEntity
						{
							Code = tp.Code,
							Name = tp.Name,
						},
					};
				return new PagedResult<TerminalDevice> { Items = query.ToList() };
			}
		}

		public PagedResult<Service> GetTerminalDeviceServices(PagedRequest<string> request)
		{
			using (var connection = GetDataConnection())
			{
				var tdss = connection.GetTable<TerminalDeviceServiceModel>();
				var ss = connection.GetTable<ServiceModel>();

				var query =
					from tds in tdss
					join s in ss on tds.ServiceCode equals s.Code
					where tds.Msisdn == request.Key
					select new Service
					{
						Code = s.Code,
						Name = s.Name,
					};
				return new PagedResult<Service> { Items = query.ToList() };
			}
		}

		protected static IDataContext GetDataConnection()
		{
			var dataConnection = new DataContext(
				new SQLiteDataProvider(),
				"Data Source=customerData.db");
			return dataConnection;
		}
	}
}
