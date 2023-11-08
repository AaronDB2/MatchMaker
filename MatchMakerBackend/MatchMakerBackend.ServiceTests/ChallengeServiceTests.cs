using AutoFixture;
using Azure;
using FluentAssertions;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using MatchMakerBackend.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ServiceTests
{
	public class ChallengeServiceTests
	{
		private readonly IFixture _fixture;
		private readonly IChallengeRepository _challengeRepository;
		private readonly IChallengeAdderService _challengeAdderService;
		private readonly IChallengeGetterService _challengeGetterService;

		private readonly Mock<IChallengeRepository> _challengeRepositoryMock;

		public ChallengeServiceTests()
		{
			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Mock challengeRepository
			_challengeRepositoryMock = new Mock<IChallengeRepository>();
			_challengeRepository = _challengeRepositoryMock.Object;

			// Initialize services
			_challengeAdderService = new ChallengeAdderService(_challengeRepository);
			_challengeGetterService = new ChallengeGetterService(_challengeRepository);
		}

		[Fact]
		public async Task GetChallengeById_WithCorrectChallengeId_ToBeSuccessfull()
		{
			//Arrange
			Guid challengeId = Guid.NewGuid();
			Challenge challenge = _fixture.Create<Challenge>(); ;
			challenge.Id = challengeId;

			//Mock GetCourseByCourseId method from CoursesRepository 
			_challengeRepositoryMock.Setup
			 (temp => temp.GetChallengeById(It.IsAny<Guid>()))
			 .ReturnsAsync(challenge);

			//Act
			var response = await _challengeGetterService.GetChallengeByChallengeId(challengeId);

			//Assert
			response.Should().NotBeNull();
			response.ChallengeId.Should().Be(challenge.Id);
		}

		[Fact]
		public async Task GetAllChallenges_Called_ToBeSuccessfull()
		{
			//Arrange
			Challenge challenge1 = _fixture.Create<Challenge>();
			Challenge challenge2 = _fixture.Create<Challenge>();
			Challenge challenge3 = _fixture.Create<Challenge>();

			List<Challenge> challenges = new List<Challenge>() { challenge1, challenge2, challenge3 };

			List<ChallengeResponse> challengeResponses = new List<ChallengeResponse>();

			challengeResponses.Add(new ChallengeResponse()
			{
				ChallengeId = challenge1.Id,
				ChallengeDescription = challenge1.ChallengeDescription,
				ChallengeTitle = challenge1.ChallengeTitle,
			});

			challengeResponses.Add(new ChallengeResponse()
			{
				ChallengeId = challenge2.Id,
				ChallengeDescription = challenge2.ChallengeDescription,
				ChallengeTitle = challenge2.ChallengeTitle,
			});

			challengeResponses.Add(new ChallengeResponse()
			{
				ChallengeId = challenge3.Id,
				ChallengeDescription = challenge3.ChallengeDescription,
				ChallengeTitle = challenge3.ChallengeTitle,
			});

			//Mock GetAllChallenges method from ChallengeRepository 
			_challengeRepositoryMock.Setup
				(temp => temp.GetAllChallenges())
				.ReturnsAsync(challenges);

			//Act
			List<ChallengeResponse> response = await _challengeGetterService.GetAllChallenges();

			//Assert
			response.Should().NotBeNull();

			// Loop over the response and check if the Ids are the same as the dummy challenges
			for (int i = 0; i < response.Count; i++)
			{
				response[i].ChallengeId.Should().Be(challenges[i].Id);
			};
		}

		[Fact]
		public async Task GetFilterdChallenges_SearchByChallengeId_ToBeSuccessfull()
		{
			//Arrange
			Challenge challenge1 = _fixture.Create<Challenge>();

			List<Challenge> challenges = new List<Challenge>() { challenge1 };

			//Mock GetFilterdChallenge method from Challenge Repository 
			_challengeRepositoryMock.Setup
			 (temp => temp.GetFilterdChallenges(It.IsAny<Expression<Func<Challenge, bool>>>()))
			 .ReturnsAsync(challenges);

			//Act
			List<ChallengeResponse> response = await _challengeGetterService.GetFilterdChallenges("ChallengeId", challenge1.Id.ToString());

			//Assert
			response.Should().NotBeNull();
			response[0].ChallengeId.Should().Be(challenge1.Id);
		}
	}
}
