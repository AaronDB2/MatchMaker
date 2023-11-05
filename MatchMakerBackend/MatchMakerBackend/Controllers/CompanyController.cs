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
		// Constructor
		public CompanyController(ICompanyAdderService companyAdderService) {
			_companyAdderService = companyAdderService;
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

			CompanyResponse response = await _companyAdderService.AddCompany(createCompanyRequest);

			return Ok(response);
		}
	}
}
