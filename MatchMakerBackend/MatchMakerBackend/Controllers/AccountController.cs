﻿using MatchMakerBackend.Core.Domain.IdentityEntities;
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

		// Constructor
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtService = jwtService;
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
	}
}
