using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// Request DTO for updating user company name
	/// </summary>
	public class UpdateUserCompanyRequest
	{
		public string CompanyName { get; set; }

		public string UserName { get; set; }
	}
}
