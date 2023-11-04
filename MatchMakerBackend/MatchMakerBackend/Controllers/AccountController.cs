using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
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

		// Constructor
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;;
		}

		/// <summary>
		/// Login end point for signing in users based on given password and email
		/// </summary>
		/// <param name="loginDTO">LoginDTO model</param>
		/// <returns>On success returns username and email</returns>
		[HttpPost]
		public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
		{
			//Validation
			if (ModelState.IsValid == false)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

			if (user == null)
			{
				return NoContent();
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				return Ok(new { userName = user.UserName, email = user.Email });
			}

			else
			{
				return Problem("Invalid email or password");
			}
		}
	}
}
