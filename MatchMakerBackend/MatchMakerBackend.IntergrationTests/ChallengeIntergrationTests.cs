using FluentAssertions;
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
	}
}
