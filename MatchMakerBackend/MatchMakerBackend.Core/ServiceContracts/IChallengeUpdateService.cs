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
	}
}
