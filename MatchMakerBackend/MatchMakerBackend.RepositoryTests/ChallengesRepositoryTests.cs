using AutoFixture;
using EntityFrameworkCoreMock;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using MatchMakerBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.RepositoryTests
{
	public class ChallengesRepositoryTests
	{
		private readonly IChallengeRepository _challengeRepository;

		private readonly IFixture _fixture;

		// Test data
		private List<Challenge> challenges = new List<Challenge>();

		Challenge testChallenge;

		// constructor
		public ChallengesRepositoryTests()
		{
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Generate test data
			for (int i = 0; i < 10; i++)
			{
				challenges.Add(_fixture.Create<Challenge>());
			}

			testChallenge = challenges[0];

			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Challenges, challenges);

			_challengeRepository = new ChallengeRepository(dbContext);
		}

		[Fact]
		public async Task GetAllChallenges_ShouldGetAllChallengesFromDataStore()
		{
			// Act
			List<Challenge> Result = await _challengeRepository.GetAllChallenges();

			// Assert
			Assert.NotEmpty(Result);
			Assert.Equal(10, Result.Count);
			Assert.Equal(Result[1].Id, challenges[1].Id);
		}

		[Fact]
		public async Task GetFilterdChallenges_ShouldGetFilterdChallengesBasedOnExpression()
		{
			// Act
			List<Challenge> returnedChallenges = await _challengeRepository.GetFilterdChallenges(temp => temp.ChallengeTitle.Contains(challenges[7].ChallengeTitle));

			// Assert
			Assert.NotEmpty(returnedChallenges);
			Assert.Equal(challenges[7].ChallengeTitle, returnedChallenges[0].ChallengeTitle);
		}

		[Fact]
		public async Task AddChallenge_ShouldAddGivenChallenge()
		{
			// Arrange
			Challenge newChallenge = _fixture.Create<Challenge>();

			// Act
			Challenge result = await _challengeRepository.AddChallenge(newChallenge);

			// Assert
			Assert.Equal(result, newChallenge);
		}

		[Fact]
		public async Task UpdateChallenge_ShouldUpdateChallengeIfExistsInDataStore()
		{
			// Arrange
			Challenge updateChallenge = _fixture.Create<Challenge>();
			updateChallenge.Id = testChallenge.Id;

			// Act
			Challenge result = await _challengeRepository.UpdateChallenge(updateChallenge);

			// Assert
			Assert.Equal(result, updateChallenge);
		}
	}
}
