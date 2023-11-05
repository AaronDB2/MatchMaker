using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for sending company entity data
	/// </summary>
	public class CompanyResponse
	{
		public string? CompanyName { get; set; }

		public string? CompanyDescription { get; set;}
	}
}
