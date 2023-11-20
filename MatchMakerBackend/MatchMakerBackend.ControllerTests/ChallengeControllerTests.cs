using AutoFixture;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.UI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ControllerTests
{
	public class ChallengeControllerTests
	{
		private readonly ChallengeController _challengeController;

		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
		private readonly Mock<IChallengeAdderService> _challengeAdderServiceMock;
		private readonly Mock<IChallengeUpdateService> _challengeUpdateServiceMock;
		private readonly Mock<IChallengeGetterService> _challengeGetterServiceMock;
		private readonly Mock<IFileService> _fileServiceMock;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IChallengeGetterService _challengeGetterService;
		private readonly IChallengeUpdateService _challengeUpdateService;
		private readonly IChallengeAdderService _challengeAdderService;
		private readonly IFixture _fixture;
		private readonly IFileService _fileService;

		// Constructor
		public ChallengeControllerTests()
		{
			// Generate a dummy IFormFile for CreateChallengeRequest DTO
			byte[] filebytes = Encoding.UTF8.GetBytes("dummy pdf");
			IFormFile file = new FormFile(new MemoryStream(filebytes), 0, filebytes.Length, "Data", "image.txt");

			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Add generated dummy form file to all fixture created CreateChallengeRequest
			_fixture.Customize<CreateChallengeRequest>(c => c.With(x => x.UploadChallengeFile, file));

			var userStore = new Mock<IUserStore<ApplicationUser>>();

			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
			_userManager = _userManagerMock.Object;

			_challengeAdderServiceMock = new Mock<IChallengeAdderService>();
			_challengeAdderService = _challengeAdderServiceMock.Object;

			_challengeGetterServiceMock = new Mock<IChallengeGetterService>();
			_challengeGetterService = _challengeGetterServiceMock.Object;

			_challengeUpdateServiceMock = new Mock<IChallengeUpdateService>();
			_challengeUpdateService = _challengeUpdateServiceMock.Object;

			_fileServiceMock = new Mock<IFileService>();
			_fileService = _fileServiceMock.Object;

			_challengeController = new ChallengeController(_userManager, _challengeAdderService, _challengeGetterService, _challengeUpdateService, _fileService);
		}

		[Fact]
		public async void CreateChallenge_ShouldSucceed_WhenRequestDataIsValid()
		{
			// Arrange
			CreateChallengeRequest requestDTO = _fixture.Create<CreateChallengeRequest>();
			ChallengeResponse response = _fixture.Create<ChallengeResponse>();
			ApplicationUser user = _fixture.Create<ApplicationUser>();

			//Mock FindByNameAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByNameAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock AddChallenge method from challengeAdderService
			_challengeAdderServiceMock.Setup
			 (temp => temp.AddChallenge(It.IsAny<CreateChallengeRequest>(), It.IsAny<ApplicationUser>()))
			.ReturnsAsync(response);

			// Act
			var result = await _challengeController.CreateChallenge(requestDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void GetFilterdChallenges_ShouldReturnChallenges_WhenRequestDataIsValid()
		{
			// Arrange
			List<ChallengeResponse> challenges = new List<ChallengeResponse>();

			// Generate test data
			for (int i = 0; i < 4; i++)
			{
				challenges.Add(_fixture.Create<ChallengeResponse>());
			}

			// Mock GetFilterdChallenges method from ChallengeGetterService
			_challengeGetterServiceMock.Setup
			 (temp => temp.GetFilterdChallenges(It.IsAny<string>(), It.IsAny<string>()))
			 .ReturnsAsync(challenges);

			// Act
			var result = await _challengeController.GetFilterdChallenges("ChallengeId", challenges[0].ChallengeId.ToString());

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
