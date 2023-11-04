using AutoFixture;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.UI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ControllerTests
{
	public class AccountControllerTests
	{
		private readonly AccountController _accountController;

		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
		private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IFixture _fixture;

		public AccountControllerTests()
		{
			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			var userStore = new Mock<IUserStore<ApplicationUser>>();

			_userManagerMock = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
			_userManager = _userManagerMock.Object;

			_signInManagerMock = new Mock<SignInManager<ApplicationUser>>(_userManager, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
			_signInManager = _signInManagerMock.Object;

			_accountController = new AccountController(_userManager, _signInManager);
		}

		[Fact]
		public async void PostLogin_ShouldReturnUsernameAndEmailWhenLoginSuccess()
		{
			// Arrange
			LoginDTO loginDTO = _fixture.Create<LoginDTO>();
			ApplicationUser user = _fixture.Create<ApplicationUser>();

			//Mock FindByEmailAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByEmailAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock FindByEmailAsync method from UserManager
			_signInManagerMock.Setup
			 (temp => temp.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
			 .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

			// Act
			var result = await _accountController.PostLogin(loginDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
