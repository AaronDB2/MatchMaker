using MatchMakerBackend.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for creating company request
	/// </summary>
	public class CreateCompanyRequest
	{
		[Required(ErrorMessage = "Company name cant be blanck")]
		public string CompanyName { get; set; }

		[Required(ErrorMessage = "Description can't be blank")]
		public string CompanyDescription { get; set; }

		/// <summary>
		/// Converts DTO object to Company entity
		/// </summary>
		/// <returns>Company object with DTO data</returns>
		public Company ToCompany()
		{
			return new Company() { CompanyName = CompanyName, CompanyDescription = CompanyDescription };
		}
	}
}
