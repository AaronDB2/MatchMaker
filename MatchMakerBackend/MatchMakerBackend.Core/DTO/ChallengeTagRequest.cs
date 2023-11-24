using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for adding tags to challenge entity
	/// </summary>
	public class ChallengeTagRequest
	{
		public string TagName { get; set; }

		public Guid ChallengeId { get; set; }
	}
}
