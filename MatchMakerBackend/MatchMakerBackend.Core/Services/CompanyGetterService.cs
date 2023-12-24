using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class CompanyGetterService : ICompanyGetterService
	{
		private readonly ICompanyRepository _companyRepository;

		// Constructor
		public CompanyGetterService(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository;
		}

		public async Task<CompanyResponse> GetCompanyByCompanyName(string companyName)
		{
			// Get company that matches companyName
			Company company = await _companyRepository.GetCompanyByCompanyName(companyName);

			// Check if there was a company found for given id
			if (company == null) throw new ArgumentNullException();

			// Make response
			CompanyResponse response = new CompanyResponse() { CompanyName = company.CompanyName, CompanyId = company.Id};

			return response;
		}
	}
}
