using AutoFixture;
using FluentAssertions;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ServiceTests
{
	public class JwtServiceTests
	{
		private readonly IFixture _fixture;
		private readonly IJwtService _jwtService;

		public JwtServiceTests() 
		{
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
		}

		[Fact]
		public async Task CreateJwtToken_WithCorrectParams_ToBeSuccessfull()
		{
			// Arrange
			ApplicationUser user = _fixture.Create<ApplicationUser>();
			IList<string> roles = new List<string>()
			{
				"Admin",
				"ImpactMaker"
			};

			// Act
			AuthenticationResponse response = _jwtService.CreateJwtToken(user, roles);

			// Assert
			response.Should().NotBeNull();
			response.Email.Should().Be(user.Email);
		}
	}
}
