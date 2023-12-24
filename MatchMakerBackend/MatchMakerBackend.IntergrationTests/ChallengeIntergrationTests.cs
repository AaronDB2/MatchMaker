using AutoFixture;
using FluentAssertions;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.IntergrationTests
{
	public class ChallengeIntergrationTests : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _client;
		private readonly IJwtService _jwtService;
		private readonly IFixture _fixture;

		// Constructor
		public ChallengeIntergrationTests(CustomWebApplicationFactory factory)
		{
			_client = factory.CreateClient();

			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Set configuration values
			var myConfiguration = new Dictionary<string, string>
			{
				{"Jwt:Issuer", "http://localhost:5063"},
				{"Jwt:Audience", "http://localhost:9500"},
				{"Jwt:EXPIRATION_MINUTES", "10"},
				{"Jwt:Key", "This is secret key for jwt"},
				{"RefreshToken:EXPIRATION_MINUTES", "60"}
			};

			// Build configuration object
			var configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(myConfiguration)
			.Build();

			// Initialize services
			_jwtService = new JwtService(configuration);

			// Create test user
			ApplicationUser user = _fixture.Create<ApplicationUser>();

			List<string> roles = new List<string>()
			{
				"Admin"
			};

			AuthenticationResponse response = _jwtService.CreateJwtToken(user, roles);

			// Set Jwt token
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
		}

		[Fact]
		public async void PostRegister_ShouldReturnAuthResponse()
		{
			// Arrange
			_client.DefaultRequestHeaders.Authorization = null;

			RegisterDTO request = new RegisterDTO()
			{
				UserName= "Test",
				Password = "Secret1",
				ConfirmPassword = "Secret1",
				Email = "test@test.com"
			};

			// Act
			HttpResponseMessage response = await _client.PostAsync("/api/account/register", new StringContent(JsonConvert.SerializeObject(request), Encoding.Default, "application/json"));

			// Assert
			response.Should().BeSuccessful();

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

		[Fact]
		public async void CreateTag_ShouldReturnTagResponse()
		{
			// Arrange
			CreateTagRequest request = new CreateTagRequest()
			{
				TagName = "testTag",
			};

			// Act
			HttpResponseMessage response = await _client.PostAsync("/api/tag/createtag", new StringContent(JsonConvert.SerializeObject(request), Encoding.Default, "application/json"));

			// Assert
			response.Should().BeSuccessful();

		}
	}


}
