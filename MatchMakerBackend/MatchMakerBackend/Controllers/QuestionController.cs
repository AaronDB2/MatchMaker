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
	public class QuestionController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IQuestionAdderService _questionAdderService;
		private readonly IQuestionGetterService _questionGetterService;

		// Constructor
		public QuestionController(
			UserManager<ApplicationUser> userManager,
			IQuestionAdderService questionAdderService,
			IQuestionGetterService questionGetterService
		)
		{
			_userManager = userManager;
			_questionAdderService = questionAdderService;
			_questionGetterService = questionGetterService;
		}

		/// <summary>
		/// Endpoint for creating questions for the data store
		/// </summary>
		/// <param name="createQuestionRequest">Request model for creating question</param>
		/// <returns>If success returns the question that was created</returns>
		[HttpPost]
		public async Task<IActionResult> CreateQuestion(CreateQuestionRequest createQuestionRequest)
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get user by username
			ApplicationUser? user = await _userManager.FindByNameAsync(createQuestionRequest.UserName);

			// Check if user had been found
			if (user == null)
			{
				return NoContent();
			}

			QuestionResponse response = await _questionAdderService.AddQuestion(createQuestionRequest, user.Id);

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
		/// Endpoint for searching questions
		/// </summary>
		/// <param name="searchBy">Param to search by</param>
		/// <param name="searchString">Value to search by</param>
		/// <returns>On success a lost of questions that match the search params</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetFilterdQuestions(string searchBy = "", string searchString = "")
		{
			// Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			// Get filterd questions
			List<QuestionResponse> filterdQuestions = await _questionGetterService.GetFilterdQuestions(searchBy, searchString);

			// Check if response is null
			if (filterdQuestions == null)
			{
				return NoContent();
			}
			else
			{
				return Ok(filterdQuestions);
			}
		}
	}
}
