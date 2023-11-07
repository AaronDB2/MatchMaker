using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// Response DTO for account entity
	/// </summary>
	public class AccountResponse
	{
		public string UserName { get; set; }

		public string Email { get; set; }
	}
}
