using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Microsoft.AspNetCore.Authorization;
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
		private readonly IChallengeGetterService _challengeGetterService;
		private readonly IChallengeUpdateService _challengeUpdateService;
		private readonly IFileService _fileService;

		// Constructor
		public ChallengeController(
			UserManager<ApplicationUser> userManager, 
			IChallengeAdderService challengeAdderService,
			IChallengeGetterService challengeGetterService,
			IChallengeUpdateService challengeUpdateService,
			IFileService fileService
		) 
		{ 
			_userManager = userManager;
			_challengeAdderService = challengeAdderService;
			_challengeGetterService = challengeGetterService;
			_challengeUpdateService = challengeUpdateService;
			_fileService = fileService;
		}

		/// <summary>
		/// Endpoint for creating challenges for the data store
		/// </summary>
		/// <param name="createChallengeRequest">Request model for creating challenge</param>
		/// <returns>If success returns the challenge that was created</returns>
		[HttpPost]
		[Route("createChallenge")]
		public async Task<IActionResult> CreateChallenge([FromForm] CreateChallengeRequest createChallengeRequest)
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Add Filename to createChallengeRequest
			createChallengeRequest.ChallengeFile = createChallengeRequest.UploadChallengeFile.FileName;

			await _fileService.UploadFile(createChallengeRequest.UploadChallengeFile);

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

		/// <summary>
		/// Endpoint for searching challenges
		/// </summary>
		/// <param name="searchBy">Param to search by</param>
		/// <param name="searchString">Value to search by</param>
		/// <returns>On success a lost of challenges that match the search params</returns>
		[HttpGet]
		[Route("getFilterdChallenges")]
		[AllowAnonymous]
		public async Task<IActionResult> GetFilterdChallenges(string searchBy = "", string searchString = "")
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}
			
			// Get filterd challenges
			List<ChallengeResponse> filterdChallenges = await _challengeGetterService.GetFilterdChallenges(searchBy, searchString);

			// Check if response is null
			if (filterdChallenges == null)
			{
				return NoContent();
			}
			else
			{
				return Ok(filterdChallenges);
			}
		}

		/// <summary>
		/// Endpoint for getting a challenge by Id
		/// </summary>
		/// <param name="challengeId">Challenge Id to get</param>
		/// <returns>On success returns a challenge that matches the given Id</returns>
		[HttpGet]
		[Route("{challengeId}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetChallenge(Guid challengeId)
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get challenge by id
			ChallengeResponse challenge = await _challengeGetterService.GetChallengeByChallengeId(challengeId);

			// Check if response is null
			if (challenge == null)
			{
				return NoContent();
			}
			else
			{
				return Ok(challenge);
			}
		}

		/// <summary>
		/// Endpoint for updating a challenge entity in data store
		/// </summary>
		/// <param name="UpdateChallengeRequest">Challenge data to update</param>
		/// <returns>On success the updated challenge</returns>
		[HttpPost]
		[Route("editchallenge")]
		public async Task<IActionResult> EditChallenge([FromForm] UpdateChallengeRequest updateChallengeRequest)
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Add Filename to updateChallengeRequest
			updateChallengeRequest.ChallengeFile = updateChallengeRequest.UploadChallengeFile.FileName;

			// Add EndResultFilename to updateChallengeRequest
			updateChallengeRequest.EndResultFileName = updateChallengeRequest.UploadEndResultFile.FileName;

			await _fileService.UploadFile(updateChallengeRequest.UploadChallengeFile);

			await _fileService.UploadFile(updateChallengeRequest.UploadEndResultFile);

			// Call service for editing challenge
			ChallengeResponse response = await _challengeUpdateService.EditChallenge(updateChallengeRequest);

			// Check if response is null
			if (response == null)
			{
				return NoContent();
			}
			else
			{
				return Ok(response);
			}
		}

		/// <summary>
		/// End point for downloading challenge files based on file name
		/// </summary>
		/// <param name="fileName">Name of the file to download</param>
		/// <returns>File that matches the given file name</returns>
		[HttpGet]
		[Route("download/{filename}")]
		[AllowAnonymous]
		public async Task<IActionResult> Download(string fileName)
		{
			var stream = _fileService.Download(fileName);

			return File(stream, "application/octet-stream", fileName);
		}
	}
}
