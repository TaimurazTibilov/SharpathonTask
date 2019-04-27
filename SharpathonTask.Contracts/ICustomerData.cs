namespace SharpathonTask.Contracts
{
	public interface ICustomerData
	{
		PagedResult<Service> GetServices(PagedRequest<string> request);
	}
}
