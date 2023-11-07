using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakerBackend.UI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IJwtService _jwtService;
		private readonly ICompanyGetterService _companyGetterService;

		// Constructor
		public AccountController(
			UserManager<ApplicationUser> userManager, 
			SignInManager<ApplicationUser> signInManager, 
			IJwtService jwtService,
			ICompanyGetterService companyGetterService
		)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtService = jwtService;
			_companyGetterService = companyGetterService;
		}

		/// <summary>
		/// Login end point for signing in users based on given password and email
		/// </summary>
		/// <param name="loginDTO">LoginDTO model</param>
		/// <returns>On success returns username and email</returns>
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
		{
			//Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

			// Check if user had been found
			if (user == null)
			{
				return NoContent();
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

			// Check if sign in was a success
			if (result.Succeeded)
			{
				// Create Jwt token
				var authenticationResponse = _jwtService.CreateJwtToken(user);

				return Ok(authenticationResponse);
			}

			else
			{
				return Problem("Invalid email or password");
			}
		}

		/// <summary>
		/// Updates user password endpoint
		/// </summary>
		/// <param name="updatePasswordRequest">Data for updating the user password</param>
		/// <returns>New Jwt cookie when success</returns>
		/// <exception cref="ArgumentNullException">Check if request data exists</exception>
		[HttpPost]
		[Route("updateUserPassword")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
		{
			// Check if updatePasswordRequest is null
			if (updatePasswordRequest == null)
			{
				throw new ArgumentNullException(nameof(updatePasswordRequest));
			}

			// Check if model state is valid
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get current user from data store
			ApplicationUser user = await _userManager.FindByNameAsync(updatePasswordRequest.UserName);

			// Check if user had been found
			if (user == null)
			{
				return NoContent();
			}

			IdentityResult result = await _userManager.ChangePasswordAsync(user, updatePasswordRequest.CurrentPassword, updatePasswordRequest.Password);

			// Check if password change was success
			if (result.Succeeded)
			{
				// Create Jwt token
				var authenticationResponse = _jwtService.CreateJwtToken(user);

				return Ok(authenticationResponse);
			}
			else
			{
				return Problem("Wrong password");
			}
		}

		/// <summary>
		/// Update company end point for updating user company
		/// </summary>
		/// <param name="updateUserCompanyRequest">UpdateUserCompanyRequest model</param>
		/// <returns>On success returns updated company name</returns>
		[HttpPost]
		[Route("updateUserCompany")]
		public async Task<IActionResult> UpdateUserCompany(UpdateUserCompanyRequest updateUserCompanyRequest)
		{
			//Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get user by username
			ApplicationUser? user = await _userManager.FindByNameAsync(updateUserCompanyRequest.UserName);

			// Check if user had been found
			if (user == null)
			{
				return NoContent();
			}

			var companyResponse = await _companyGetterService.GetCompanyByCompanyName(updateUserCompanyRequest.CompanyName);

			// Check if company was found
			if (companyResponse == null)
			{
				return NoContent();
			}

			// Set company on user entity
			user.CompanyId = companyResponse.CompanyId;

			// Update the user in data store
			IdentityResult result = await _userManager.UpdateAsync(user);

			// Check if user was updated
			if (result.Succeeded)
			{
				return Ok(companyResponse);
			}
			else
			{
				return Problem("Company is invalid");
			}
		}

		/// <summary>
		/// Endpoint for creating a new user account without sending a new JWT token
		/// This endpoint is only for creating a new user account.
		/// </summary>
		/// <param name="registerDTO">DTO model to create a new user account from</param>
		/// <returns>On success created account username and email</returns>
		[HttpPost]
		[Route("createUserAccount")]
		public async Task<IActionResult> CreateUserAccount(RegisterDTO registerDTO)
		{
			//Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Create new ApplicationUser based on data from RegisterDTO
			ApplicationUser user = new ApplicationUser();
			user.UserName = registerDTO.UserName;
			user.Email = registerDTO.Email;

			// Create new User
			IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

			// Check if user was updated
			if (result.Succeeded)
			{
				// Check if user needs the admin role
				if (registerDTO.Admin != null)
				{
					IdentityResult resultAddedToRole = await _userManager.AddToRoleAsync(user, "Admin");
				}

				// Create Response
				AccountResponse accountResponse = new AccountResponse()
				{
					UserName = registerDTO.UserName,
					Email = registerDTO.Email,
				};

				return Ok(accountResponse);
			}
			else
			{
				return Problem("Account is invalid");
			}
		}
	}
}
