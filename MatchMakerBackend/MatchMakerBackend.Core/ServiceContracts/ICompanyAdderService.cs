using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	public interface ICompanyAdderService
	{
		/// <summary>
		/// Calls repository for creating a new company entity
		/// </summary>
		/// <param name="createCompanyRequest">Data to create a new company from</param>
		/// <returns>Created company</returns>
		Task<CompanyResponse> AddCompany(CreateCompanyRequest? createCompanyRequest);
	}
}
