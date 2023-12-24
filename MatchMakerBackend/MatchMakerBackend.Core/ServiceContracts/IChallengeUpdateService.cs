using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining challenge update service
	/// </summary>
	public interface IChallengeUpdateService
	{
		/// <summary>
		/// Updates a challenge with given data
		/// </summary>
		/// <param name="updateChallengeRequest">Challenge data to update</param>
		/// <returns>Updated challenge</returns>
		Task<ChallengeResponse> EditChallenge(UpdateChallengeRequest updateChallengeRequest);

		/// <summary>
		/// Adds a tag to the challenge
		/// </summary>
		/// <param name="challengeTagRequest">ChallengeTagRequest</param>
		/// <param name="tag">Tag to add to the challenge</param>
		/// <returns>ChallengeResponse</returns>
		Task<ChallengeResponse> AddChallengeTag(ChallengeTagRequest challengeTagRequest, Tag tag);
	}
}
