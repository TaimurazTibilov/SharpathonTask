using System;
using System.Collections.Generic;
using System.Text;

namespace SharpathonTask.Contracts
{
	/// <summary>Тип абонента</summary>
	public enum CustomerType
	{
		/// <summary>Физическое лицо</summary>
		Individual = 1,

		/// <summary>Юридическое лицо</summary>
		Organization = 2,
	}
}
