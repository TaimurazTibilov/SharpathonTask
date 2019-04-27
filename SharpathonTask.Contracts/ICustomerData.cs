namespace SharpathonTask.Contracts
{
	public interface ICustomerData
	{
		/// <summary>Получить абонента по его уникальному идентификатору</summary>
		/// <remarks>В связи с ограничением в клиенте на то, что параметры метода должны быть классами, входной параметр customerId заворачивается в класс-обёртку</remarks>
		Customer GetCustomer(PagedRequest<int> customerId);

		/// <summary>
		/// Получить абонентов, имя которых подходит под переданную "маску".
		/// Маска представляет из себя строку, которая должна входить в имя абонента.
		/// При поиске регистр не учитывается. Пустая строка означает, что нужно вернуть всех абонентов, какие только есть
		/// </summary>
		PagedResult<Customer> GetCustomersByNameMask(PagedRequest<string> request);

		/// <summary>Получить все контракты абонента с данным идентификатором</summary>
		PagedResult<Contract> GetCustomerContracts(PagedRequest<int> request);

		/// <summary>Получить все лицевые счета по переданному идентификатору контракта</summary>
		PagedResult<PersonalAccount> GetContractPersonalAccounts(PagedRequest<string> request);

		/// <summary>Получить все приложения обслуживания, принадлежащие к лицевому счёту с переданным идентификатором</summary>
		PagedResult<TerminalDevice> GetPersonalAccountTerminalDevices(PagedRequest<string> request);

		/// <summary>Получить услуги на приложении обслуживания. Ключ - дентификатор приложения обслуживания</summary>
		PagedResult<Service> GetTerminalDeviceServices(PagedRequest<string> request);
	}
}
