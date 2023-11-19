using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MatchMakerBackend.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagController : ControllerBase
	{
		private readonly ITagAdderService _tagAdderService;
		// Constructor
		public TagController(ITagAdderService tagAdderService)
		{
			_tagAdderService = tagAdderService;
		}

		/// <summary>
		/// Controller for creating tag entity
		/// </summary>
		/// <param name="createTagRequest">Request DTO for creating tag</param>
		/// <returns>On success the created tag</returns>
		[HttpPost]
		[Route("createTag")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateTag(CreateTagRequest createTagRequest)
		{
			//Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			TagResponse response = await _tagAdderService.AddTag(createTagRequest);

			return Ok(response);
		}
	}
}
