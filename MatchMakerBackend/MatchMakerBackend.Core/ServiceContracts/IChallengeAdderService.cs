using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining create challenge service functionality
	/// </summary>
	public interface IChallengeAdderService
	{
		/// <summary>
		/// Calls repository for creating a new challenge entity and validates data
		/// </summary>
		/// <param name="createChallengeRequest">Data to create a new challenge from</param>
		/// <param name="user">User that creates the challenge entity</param>
		/// <returns>Created challenge</returns>
		Task<ChallengeResponse> AddChallenge(CreateChallengeRequest? createChallengeRequest, ApplicationUser user);
	}
}
