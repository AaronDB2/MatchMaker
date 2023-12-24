using AutoFixture;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using MatchMakerBackend.UI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace MatchMakerBackend.ServiceTests
{
	public class CompanyServiceTests
	{
		private readonly IFixture _fixture;
		private readonly ICompanyRepository _companyRepository;
		private readonly ICompanyAdderService _companyAdderService;
		private readonly ICompanyGetterService _companyGetterService;

		private readonly Mock<ICompanyRepository> _companyRepositoryMock;

		public CompanyServiceTests()
		{
			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Mock companyRepository
			_companyRepositoryMock = new Mock<ICompanyRepository>();
			_companyRepository = _companyRepositoryMock.Object;

			// Initialize services
			_companyAdderService = new CompanyAdderService(_companyRepository);
			_companyGetterService = new CompanyGetterService(_companyRepository);
		}

		[Fact]
		public async Task AddCompany_CompanyDetailsComplete_ToBeSuccessfull()
		{
			//Arrange
			CreateCompanyRequest createCompanyRequest = _fixture.Create<CreateCompanyRequest>();
			Company company = createCompanyRequest.ToCompany();
			CompanyResponse companyResponse = new CompanyResponse()
			{
				CompanyName = company.CompanyName,
				CompanyDescription = company.CompanyDescription,
			};

			//Mock AddCompany method from CompanyRepository 
			_companyRepositoryMock.Setup
			 (temp => temp.AddCompany(It.IsAny<Company>()))
			 .ReturnsAsync(company);

			//Act
			CompanyResponse result = await _companyAdderService.AddCompany(createCompanyRequest);

			//Assert
			result.CompanyName.Should().Be(companyResponse.CompanyName);

		}

		[Fact]
		public async Task GetCompanyByCompanyName_ShouldGetCompanieByName_IfThereAreCompaniesThatMatchTheGivenName()
		{
			//Arrange
			Company company = _fixture.Create<Company>();

			//Mock GetCompanyByCompanyName method from CompanyRepository 
			_companyRepositoryMock.Setup
			 (temp => temp.GetCompanyByCompanyName(It.IsAny<string>()))
			 .ReturnsAsync(company);

			//Act
			CompanyResponse result = await _companyGetterService.GetCompanyByCompanyName(company.CompanyName);

			//Assert
			result.CompanyName.Should().Be(company.CompanyName);

		}
	}
}
