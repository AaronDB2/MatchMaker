using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for updating challenge
	/// </summary>
	public class UpdateChallengeRequest
	{
		public Guid challengeId { get; set; }

		public string ChallengeTitle { get; set; }

		public string ChallengeDescription { get; set; }

		public string? ChallengeFile { get; set; }

		public string ChallengeViewStatus { get; set; }

		public string ChallengeProgressionStatus { get; set; }

		public DateTime EndDate { get; set; }
		public IFormFile? UploadChallengeFile { get; set; }

		/// <summary>
		/// Converts Request to Challenge model
		/// </summary>
		/// <returns>Challenge with request data</returns>
		public Challenge ToChallenge()
		{
			return new Challenge()
			{
				Id = challengeId,
				ChallengeTitle = ChallengeTitle,
				ChallengeDescription = ChallengeDescription,
				ChallengeFileName = ChallengeFile,
				ViewStatus = ChallengeViewStatus,
				ProgressionStatus = ChallengeProgressionStatus,
				EndDate = EndDate,
			};
		}
	}
}
