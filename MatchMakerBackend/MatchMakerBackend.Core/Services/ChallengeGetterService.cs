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
	public class ChallengeGetterService : IChallengeGetterService
	{
		private readonly IChallengeRepository _challengeRepository;

		// Constructor
		public ChallengeGetterService(IChallengeRepository challengeRepository)
		{
			_challengeRepository = challengeRepository;
		}

		public async Task<List<ChallengeResponse?>> GetAllChallenges()
		{
			List<Challenge> challenges = await _challengeRepository.GetAllChallenges();

			List<ChallengeResponse> response = new List<ChallengeResponse>();

			// Convert all challenges into ChallengeResponse objects and add them to the response list
			foreach (Challenge challenge in challenges)
			{
				ChallengeResponse challengeResponse = new ChallengeResponse()
				{
					ChallengeTitle= challenge.ChallengeTitle,
					ChallengeDescription= challenge.ChallengeDescription,
					ChallengeId= challenge.Id,
				};

				response.Add(challengeResponse);
			}

			return response;
		}

		public async Task<ChallengeResponse?> GetChallengeByChallengeId(Guid challengeId)
		{
			// Check if Id is empty
			if (challengeId == Guid.Empty) throw new ArgumentNullException(nameof(challengeId));

			Challenge challenge = await _challengeRepository.GetChallengeById(challengeId);

			// Check if there was a challenge found for given id
			if (challenge == null) throw new ArgumentNullException();

			ChallengeResponse response = new ChallengeResponse()
			{
				ChallengeId = challenge.Id,
				ChallengeTitle = challenge.ChallengeTitle,
				ChallengeDescription = challenge.ChallengeDescription,
				ContactPersonId = challenge.ContactPerson.Id,
				CompanyId = challenge.CompanyId,
				ProgressionStatus = challenge.ProgressionStatus,
				ViewStatus = challenge.ViewStatus,
				DateSubmitted = challenge.DateSubmitted,
				EndDate = challenge.EndDate,
				ChallengeFileName = challenge.ChallengeFileName,
				EndResultFileName = challenge.ResultFileName
			};

			return response;
		}

		public async Task<List<ChallengeResponse?>> GetFilterdChallenges(string searchBy, string? searchString)
		{
			List<Challenge>? challenges = searchBy switch
			{
				nameof(ChallengeResponse.ChallengeId) =>
				 await _challengeRepository.GetFilterdChallenges(temp =>
				 temp.Id.ToString().Contains(searchString)),

				nameof(ChallengeResponse.ChallengeTitle) =>
				 await _challengeRepository.GetFilterdChallenges(temp =>
				 temp.ChallengeTitle.Contains(searchString)),

				_ => await _challengeRepository.GetAllChallenges()
			};

			List<ChallengeResponse> response = new List<ChallengeResponse>();

			// Create a ChallengeResponse for each found challenge
			foreach (Challenge challenge in challenges)
			{
				ChallengeResponse challengeResponse = new ChallengeResponse()
				{
					ChallengeTitle= challenge.ChallengeTitle,
					ChallengeDescription= challenge.ChallengeDescription,
					ChallengeId = challenge.Id,
				};

				response.Add(challengeResponse);
			}

			return response;
		}
	}
}
