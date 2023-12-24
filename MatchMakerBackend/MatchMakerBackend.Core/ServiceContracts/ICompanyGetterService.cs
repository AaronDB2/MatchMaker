using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining methods for company getter service
	/// </summary>
	public interface ICompanyGetterService
	{
		/// <summary>
		/// Validates data and calls company repository to retrieve company for the given company name.
		/// </summary>
		/// <param name="courseId">Course id to search assignments for</param>
		/// <returns>List of all the assignments that match the given course id</returns>
		Task<CompanyResponse> GetCompanyByCompanyName(string companyName);
	}
}
