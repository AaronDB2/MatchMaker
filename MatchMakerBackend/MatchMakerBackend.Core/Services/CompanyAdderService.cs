using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	/// <summary>
	/// Service for adding company to data store
	/// </summary>
	public class CompanyAdderService : ICompanyAdderService
	{
		private readonly ICompanyRepository _companyRepository;

		// Constructor
		public CompanyAdderService(ICompanyRepository companyRepository) 
		{
			_companyRepository = companyRepository;
		}

		public async Task<CompanyResponse> AddCompany(CreateCompanyRequest? createCompanyRequest, Tag tag = null)
		{
			// Convert response to company
			Company company = createCompanyRequest.ToCompany();

			//generate company Id
			company.Id = Guid.NewGuid();

			// Add tag
			if (tag != null)
			{
				company.Tags.Add(tag);
			}

			// Add company to data store
			await _companyRepository.AddCompany(company);

			// Generate CompanyResponse
			CompanyResponse companyResponse = new CompanyResponse() { CompanyName = company.CompanyName, CompanyDescription = company.CompanyDescription };

			return companyResponse;
		}
	}
}
