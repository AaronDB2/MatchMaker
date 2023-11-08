using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining challenge getter service functionality
	/// </summary>
	public interface IChallengeGetterService
	{
		/// <summary>
		/// Gets a challenge from the data store that matches the challenge id
		/// </summary>
		/// <param name="challengeId">Id of the challenge you want to retrieve from data store</param>
		/// <returns>Challenge from data store as ChallengeResponse</returns>
		Task<ChallengeResponse?> GetChallengeByChallengeId(Guid challengeId);

		/// <summary>
		/// Gets all the challenge entities from data store
		/// </summary>
		/// <returns>List of all the challenges as RourseResponse</returns>
		Task<List<ChallengeResponse?>> GetAllChallenges();

		/// <summary>
		/// Gets the challenges that matches the search criteria
		/// </summary>
		/// <param name="searchBy">Value to search by</param>
		/// <param name="searchString">Search value</param>
		/// <returns>List of challenges as ChallengeResponse that matched the search criteria</returns>
		Task<List<ChallengeResponse?>> GetFilterdChallenges(string searchBy, string? searchString);
	}
}
