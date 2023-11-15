using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchMakerBackend.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyAdderService _companyAdderService;
		private readonly ITagGetterService _tagGetterService;

		// Constructor
		public CompanyController(
			ICompanyAdderService companyAdderService,
			ITagGetterService tagGetterService
		) 
		{
			_companyAdderService = companyAdderService;
			_tagGetterService = tagGetterService;
		}

		/// <summary>
		/// Controller for creating company entity
		/// </summary>
		/// <param name="createCompanyRequest">Request DTO for creating company</param>
		/// <returns>On success the created company</returns>
		[HttpPost]
		[Route("createCompany")]
		public async Task<IActionResult> CreateCompany(CreateCompanyRequest createCompanyRequest)
		{
			//Validation
			if (!ModelState.IsValid)
			{
				string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				return Problem(errorMessage);
			}

			if (createCompanyRequest.TagName != null)
			{
				// Find tag that matches the name
				// (should change this to a get name method instead of filter)
				List<TagResponse?>? tagResponse = await _tagGetterService.GetFilterdTags("TagName", createCompanyRequest.TagName);

				// Check if tag had been found
				if (tagResponse[0] == null)
				{
					return NoContent();
				} else
				{
					CompanyResponse responseWithTag = await _companyAdderService.AddCompany(createCompanyRequest, tagResponse[0].Tag);

					return Ok(responseWithTag.CompanyName);
				}
			} else
			{
				// Create company without tag
				CompanyResponse response = await _companyAdderService.AddCompany(createCompanyRequest);

				return Ok(response.CompanyName);
			}
		}
	}
}
