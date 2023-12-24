using AutoFixture;
using EntityFrameworkCoreMock;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using MatchMakerBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.RepositoryTests
{
	public class CompanyRepositoryTests
	{
		private readonly ICompanyRepository _companyRepository;

		private readonly IFixture _fixture;

		// Test data
		private List<Company> companies = new List<Company>();

		Company testCompany;

		// constructor
		public CompanyRepositoryTests()
		{
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Generate test data
			for (int i = 0; i < 10; i++)
			{
				companies.Add(_fixture.Create<Company>());
			}

			testCompany = companies[0];

			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Companies, companies);

			_companyRepository = new CompanyRepository(dbContext);
		}

		[Fact]
		public async Task GetAllCompanies_ShouldGetAllCompaniesFromDataStore()
		{
			// Act
			List<Company> Result = await _companyRepository.GetAllCompanies();

			// Assert
			Assert.NotEmpty(Result);
			Assert.Equal(10, Result.Count);
			Assert.Equal(Result[1].Id, companies[1].Id);
		}

		[Fact]
		public async Task GetFilterdCompanies_ShouldGetFilterdCompaniesBasedOnExpression()
		{
			// Act
			List<Company> returnedCompanies = await _companyRepository.GetFilterdCompanies(temp => temp.CompanyName.Contains(companies[7].CompanyName));

			// Assert
			Assert.NotEmpty(returnedCompanies);
			Assert.Equal(companies[7].CompanyName, returnedCompanies[0].CompanyName);
		}

		[Fact]
		public async Task AddCompany_ShouldAddGivenCompany()
		{
			// Arrange
			Company newCompany = _fixture.Create<Company>();

			// Act
			Company result = await _companyRepository.AddCompany(newCompany);

			// Assert
			Assert.Equal(result, newCompany);
		}

		[Fact]
		public async Task UpdateCompany_ShouldUpdateCompanyIfExistsInDataStore()
		{
			// Arrange
			Company updateCompany = _fixture.Create<Company>();
			updateCompany.Id = testCompany.Id;

			// Act
			Company result = await _companyRepository.UpdateCompany(updateCompany);

			// Assert
			Assert.Equal(result.CompanyName, updateCompany.CompanyName);
		}

		[Fact]
		public async Task GetCompanyByCompanyName_ShouldGetCompanyByGivenName_IfExistsInDataStoreAndNameIsValid()
		{
			// Act
			Company result = await _companyRepository.GetCompanyByCompanyName(testCompany.CompanyName);

			// Assert
			Assert.Equal(result.CompanyName, testCompany.CompanyName);
		}
	}
}
