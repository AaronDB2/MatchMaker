using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakerBackend.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChallengeController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IChallengeAdderService _challengeAdderService;

		//Constructor
		public ChallengeController(UserManager<ApplicationUser> userManager, IChallengeAdderService challengeAdderService) 
		{ 
			_userManager = userManager;
			_challengeAdderService = challengeAdderService;
		}

		/// <summary>
		/// Endpoint for creating challenges for the data store
		/// </summary>
		/// <param name="createChallengeRequest">Request model for creating challenge</param>
		/// <returns>If success returns the challenge that was created</returns>
		[HttpPost]
		[Route("createChallenge")]
		public async Task<IActionResult> CreateChallenge(CreateChallengeRequest createChallengeRequest)
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get user by username
			ApplicationUser? user = await _userManager.FindByNameAsync(createChallengeRequest.UserName);

			// Check if user had been found
			if (user == null)
			{
				return NoContent();
			}

			ChallengeResponse response = await _challengeAdderService.AddChallenge(createChallengeRequest, user);

			// Check if response is null
			if (response == null)
			{
				return NoContent();
			} else
			{
				return Ok(response);
			}
		}
	}
}
