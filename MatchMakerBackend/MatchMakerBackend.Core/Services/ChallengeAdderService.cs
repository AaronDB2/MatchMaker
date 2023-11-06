using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class ChallengeAdderService : IChallengeAdderService
	{
		private readonly IChallengeRepository _challengeRepository;

		// Constructor
		public ChallengeAdderService(IChallengeRepository challengeRepository) 
		{ 
			_challengeRepository = challengeRepository;
		}

		public async Task<ChallengeResponse> AddChallenge(CreateChallengeRequest? createChallengeRequest, ApplicationUser user)
		{
			// Convert response to challenge
			Challenge challenge = createChallengeRequest.ToChallenge(user);

			//generate Challenge Id
			challenge.Id = Guid.NewGuid();

			// Add challenge to data store
			await _challengeRepository.AddChallenge(challenge);

			// Generate ChallengeResponse
			ChallengeResponse challengeResponse = new ChallengeResponse() { ChallengeTitle = challenge.ChallengeTitle };

			return challengeResponse;
		}
	}
}
