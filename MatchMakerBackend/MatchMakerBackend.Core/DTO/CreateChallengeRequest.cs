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
	/// Request DTO for creating challenge
	/// </summary>
	public class CreateChallengeRequest
	{
		public string ChallengeTitle { get; set; }

		public string ChallengeDescription { get; set;}

		public string? ChallengeFile { get; set;}

		public string ChallengeViewStatus { get; set;}

		public string ChallengeProgressionStatus { get; set;}

		public DateTime EndDate { get; set;}

		public string UserName { get; set;}

		public DateTime DateSubmitted { get; set; }

		public IFormFile? UploadChallengeFile { get; set;}

		/// <summary>
		/// Converts Request to Challenge model
		/// </summary>
		/// <param name="user">User that created the challenge</param>
		/// <returns>Challenge with request data</returns>
		public Challenge ToChallenge(ApplicationUser user)
		{
			return new Challenge() {
				ChallengeTitle = ChallengeTitle,
				ChallengeDescription = ChallengeDescription,
				ChallengeFileName = ChallengeFile,
				ViewStatus = ChallengeViewStatus,
				ProgressionStatus = ChallengeProgressionStatus,
				EndDate = EndDate,
				DateSubmitted = DateSubmitted,
				CompanyId = (Guid)user.CompanyId,
				ContactPerson = user
			};
		}
	}
}
