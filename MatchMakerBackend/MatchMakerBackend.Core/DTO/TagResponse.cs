using MatchMakerBackend.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for tag response
	/// </summary>
	public class TagResponse
	{
		public string? TagName { get; set; }

		public Tag? Tag { get; set;}
	}
}
