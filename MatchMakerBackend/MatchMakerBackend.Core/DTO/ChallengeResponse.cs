using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// Response DTO for challenges
	/// </summary>
	public class ChallengeResponse
	{
		public string? ChallengeTitle { get; set; }

		public Guid? ChallengeId { get; set; }

		public string? ChallengeDescription { get; set; }

		public string? ChallengeFileName { get; set; }

		public string? EndResultFileName { get;set; }

		public string? ViewStatus { get; set; }

		public string? ProgressionStatus { get; set; }

		public Guid? CompanyId { get; set; }

		public Guid? ContactPersonId { get;set; }

		public DateTime? DateSubmitted { get; set; }

		public DateTime? EndDate { get; set;}
	}
}
