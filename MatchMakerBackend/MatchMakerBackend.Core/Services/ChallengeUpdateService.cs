using MatchMakerBackend.Core.Domain.Entities;
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
	public class ChallengeUpdateService : IChallengeUpdateService
	{
		private readonly IChallengeRepository _challengeRepository;

		public ChallengeUpdateService(IChallengeRepository challengeRepository)
		{
			_challengeRepository = challengeRepository;
		}

		public async Task<ChallengeResponse> AddChallengeTag(ChallengeTagRequest challengeTagRequest, Tag tag)
		{
			// Get challenge from data store
			Challenge challengeToBeUpdated = await _challengeRepository.GetChallengeById(challengeTagRequest.ChallengeId);

			// Check if challenge was found
			if (challengeToBeUpdated == null)
			{
				throw new ArgumentNullException(nameof(challengeToBeUpdated));
			}

			Challenge updatedChallenge = await _challengeRepository.UpdateChallenge(challengeToBeUpdated, tag);

			return new ChallengeResponse()
			{
				ChallengeId = updatedChallenge.Id
			};
		}

		public async Task<ChallengeResponse> EditChallenge(UpdateChallengeRequest updateChallengeRequest)
		{
			// Check if editCourseRequest is null
			if (updateChallengeRequest == null)
			{
				throw new ArgumentNullException(nameof(updateChallengeRequest));
			}

			// Get challenge from data store
			Challenge challengeToBeUpdated = await _challengeRepository.GetChallengeById(updateChallengeRequest.challengeId);

			// Check if challenge was found
			if (challengeToBeUpdated == null)
			{
				throw new ArgumentNullException(nameof(challengeToBeUpdated));
			}

			// Convert request data to challenge
			Challenge challengeUpdateData = updateChallengeRequest.ToChallenge();

			// Set missing fields
			challengeUpdateData.ContactPerson = challengeToBeUpdated.ContactPerson;
			challengeUpdateData.CompanyId = challengeToBeUpdated.CompanyId;
			challengeUpdateData.DateSubmitted = challengeToBeUpdated.DateSubmitted;

			// Update challenge
			Challenge challengeUpdated = await _challengeRepository.UpdateChallenge(challengeUpdateData, null);

			// Check if challenge was updated
			if (challengeUpdated == null)
			{
				throw new ArgumentNullException(nameof(challengeUpdated));
			}

			ChallengeResponse response = new ChallengeResponse()
			{
				ChallengeId = challengeUpdated.Id
			};

			return response;
		}
	}
}
