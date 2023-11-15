using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// Request DTO for handling user entities and tags
	/// </summary>
	public class UserTagRequest
	{
		public string TagName { get; set; }

		public string UserName { get; set; }
	}
}
