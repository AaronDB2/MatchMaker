using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using MatchMakerBackend.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Infrastructure.Repositories
{
	/// <summary>
	/// Company repository inherits from ICompanyRepository
	/// </summary>
	public class CompanyRepository : ICompanyRepository
	{
		private readonly ApplicationDbContext _db;

		public CompanyRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Add given company to the data store
		/// </summary>
		/// <param name="company">Company to add to the data store</param>
		/// <returns>Added company object</returns>
		public async Task<Company> AddCompany(Company company)
		{
			_db.Companies.Add(company);
			await _db.SaveChangesAsync();

			return company;
		}

		/// <summary>
		/// Get all companies from data store
		/// </summary>
		/// <returns>List of companies</returns>
		public async Task<List<Company>?> GetAllCompanies()
		{
			return await _db.Companies.ToListAsync();
		}

		/// <summary>
		/// Get company by company name
		/// </summary>
		/// <param name="companyName">Name of the company to retrieve out the data store</param>
		/// <returns>If found the company that matches the company name</returns>
		public async Task<Company?> GetCompanyByCompanyName(string companyName)
		{
			return await _db.Companies.FirstOrDefaultAsync(company => company.CompanyName == companyName);
		}

		/// <summary>
		/// Get filterd companies from data store
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>Filterd companies</returns>
		public async Task<List<Company>?> GetFilterdCompanies(Expression<Func<Company, bool>> predicate)
		{
			return await _db.Companies.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Updates entire company entity in data store
		/// </summary>
		/// <param name="company">company to update</param>
		/// <returns>Updated company or if not found returns given company</returns>
		public async Task<Company> UpdateCompany(Company company)
		{
			// Find matching company in db
			Company? matchingCompany = await _db.Companies.FirstOrDefaultAsync(temp => temp.Id == company.Id);

			// If there is no matching company in db return company
			if (matchingCompany == null) return company;

			// Update courseText and CourseFileName
			matchingCompany = company;

			await _db.SaveChangesAsync();

			return matchingCompany;
		}
	}
}
