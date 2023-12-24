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
	public class AccountControllerTests
	{
		private readonly AccountController _accountController;

		private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
		private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
		private readonly Mock<IJwtService> _jwtServiceMock;
		private readonly Mock<ICompanyGetterService> _companyGetterServiceMock;
		private readonly Mock<ITagGetterService> _tagGetterServiceMock;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IJwtService _jwtService;
		private readonly ICompanyGetterService _companyGetterService;
		private readonly ITagGetterService _tagGetterService;
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

			_jwtServiceMock = new Mock<IJwtService>();
			_jwtService = _jwtServiceMock.Object;

			_companyGetterServiceMock = new Mock<ICompanyGetterService>();
			_companyGetterService = _companyGetterServiceMock.Object;

			_tagGetterServiceMock = new Mock<ITagGetterService>();
			_tagGetterService = _tagGetterServiceMock.Object;

			_accountController = new AccountController(_userManager, _signInManager, _jwtService, _companyGetterService, _tagGetterService);
		}

		[Fact]
		public async void PostLogin_ShouldReturnUsernameAndEmailWhenLoginSuccess()
		{
			// Arrange
			LoginDTO loginDTO = _fixture.Create<LoginDTO>();
			ApplicationUser user = _fixture.Create<ApplicationUser>();
			AuthenticationResponse authResponse = _fixture.Create<AuthenticationResponse>();

			//Mock FindByEmailAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByEmailAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock UpdateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.UpdateAsync(It.IsAny<ApplicationUser>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock FindByEmailAsync method from SignInManager
			_signInManagerMock.Setup
			 (temp => temp.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
			 .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

			//Mock CreateJwtToken method from JwtService
			_jwtServiceMock.Setup
			 (temp => temp.CreateJwtToken(It.IsAny<ApplicationUser>(), It.IsAny<IList<string>>()))
			.Returns(authResponse);

			// Act
			var result = await _accountController.PostLogin(loginDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void PostRegister_ShouldReturnAuthResponse_OnSuccess()
		{
			// Arrange
			RegisterDTO registerDTO = _fixture.Create<RegisterDTO>();
			IList<string> roles = _fixture.Create<IList<string>>();
			AuthenticationResponse authenticationResponse = _fixture.Create<AuthenticationResponse>();

			//Mock CreateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock AddToRoleAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock UpdateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.UpdateAsync(It.IsAny<ApplicationUser>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock GetRolesAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.GetRolesAsync(It.IsAny<ApplicationUser>()))
			 .ReturnsAsync(roles);

			//Mock CreateJwtToken method from JwtService
			_jwtServiceMock.Setup
			 (temp => temp.CreateJwtToken(It.IsAny<ApplicationUser>(), It.IsAny<IList<string>>()))
			.Returns(authenticationResponse);

			// Act
			var result = await _accountController.PostRegister(registerDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void UpdatePassword_ShouldReturnOkResponseWhenProvidedWithCorrectValues()
		{
			// Arrange
			ApplicationUser user = _fixture.Create<ApplicationUser>();
			UpdatePasswordRequest requestDTO = _fixture.Create<UpdatePasswordRequest>();
			AuthenticationResponse responseDTO = _fixture.Create<AuthenticationResponse>();

			//Mock FindByNameAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByNameAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock ChangePasswordAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock CreateJwtToken method from jwtService
			_jwtServiceMock.Setup
			 (temp => temp.CreateJwtToken(It.IsAny<ApplicationUser>(), It.IsAny<IList<string>>()))
			 .Returns(responseDTO);

			// Act
			var result = await _accountController.UpdatePassword(requestDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void CreateUserAccount_ShouldReturnOkResponseWhenProvidedWithCorrectValues()
		{
			// Arrange
			RegisterDTO registerDTO = _fixture.Create<RegisterDTO>();

			//Mock CreateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
			 .ReturnsAsync(IdentityResult.Success);

			//Mock AddToRoleAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
			 .ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _accountController.CreateUserAccount(registerDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void UpdateUserCompany_ShouldReturnOkResponseWhenProvidedWithCorrectValues()
		{
			// Arrange
			UpdateUserCompanyRequest requestDTO = _fixture.Create<UpdateUserCompanyRequest>();
			ApplicationUser user = _fixture.Create<ApplicationUser>();
			CompanyResponse companyResponse = _fixture.Create<CompanyResponse>();

			//Mock FindByNameAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByNameAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock GetCompanyByCompanyName method from CompanyGetterService
			_companyGetterServiceMock.Setup
			 (temp => temp.GetCompanyByCompanyName(It.IsAny<string>()))
			 .ReturnsAsync(companyResponse);

			//Mock UpdateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.UpdateAsync(It.IsAny<ApplicationUser>()))
			 .ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _accountController.UpdateUserCompany(requestDTO);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[Fact]
		public async void AddUserTag_ShouldReturnOkResponse_OnSuccess()
		{
			// Arrange
			UserTagRequest userTagRequest = _fixture.Create<UserTagRequest>();
			ApplicationUser user = _fixture.Create<ApplicationUser>();
			List<TagResponse?>? tags = _fixture.Create<List<TagResponse?>?>();

			//Mock FindByNameAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.FindByNameAsync(It.IsAny<string>()))
			 .ReturnsAsync(user);

			//Mock GetFilterdTags method from TagGetterService
			_tagGetterServiceMock.Setup
			 (temp => temp.GetFilterdTags(It.IsAny<string>(), It.IsAny<string>()))
			 .ReturnsAsync(tags);

			//Mock UpdateAsync method from UserManager
			_userManagerMock.Setup
			 (temp => temp.UpdateAsync(It.IsAny<ApplicationUser>()))
			 .ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _accountController.AddUserTag(userTagRequest);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
	}
}
