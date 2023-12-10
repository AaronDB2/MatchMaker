using FluentAssertions;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.IntergrationTests
{
	public class ChallengeIntergrationTests : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _client;

		// Constructor
		public ChallengeIntergrationTests(CustomWebApplicationFactory factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async void GetFilterdChallenges_ShouldReturnListOfChallengeResponse()
		{
			// Arrange

			// Act
			HttpResponseMessage response = await _client.GetAsync("/api/challenge/getFilterdChallenges?searchBy=ChallengeTitle&searchString=test");

			// Assert
			response.Should().BeSuccessful();
		}

		[Fact]
		public async void GetChallenge_ShouldReturnChallengeResponse()
		{
			// Arrange

			// Act
			HttpResponseMessage response = await _client.GetAsync("/api/challenge/68977C78-A753-463E-ACE3-BA20ED1E5D6E");

			// Assert
			response.Should().BeSuccessful();
		}
	}


}
