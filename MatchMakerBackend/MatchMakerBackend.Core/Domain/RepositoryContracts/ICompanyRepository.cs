using MatchMakerBackend.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Company repository interface for defining company repository functions
	/// </summary>
	public interface ICompanyRepository
	{
		/// <summary>
		/// Returns a list of all the companies
		/// </summary>
		/// <returns>List of companies</returns>
		Task<List<Company>?> GetAllCompanies();

		/// <summary>
		/// Returns all company objects from data store based on given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>List of company objects from data store that match the given expression</returns>
		Task<List<Company>?> GetFilterdCompanies(Expression<Func<Company, bool>> predicate);

		/// <summary>
		/// Adds a new company object to the data store
		/// </summary>
		/// <param name="company">company object to add</param>
		/// <returns>The company object that was added to the data store</returns>
		Task<Company> AddCompany(Company company);

		/// <summary>
		/// Update company data
		/// </summary>
		/// <param name="company">Company object to update</param>
		/// <returns>Updated company object</returns>
		Task<Company> UpdateCompany(Company company);
	}
}
