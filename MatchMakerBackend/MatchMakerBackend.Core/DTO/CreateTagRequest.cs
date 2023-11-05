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
	/// Create tag requst DTOo
	/// </summary>
	public class CreateTagRequest
	{
		[Required(ErrorMessage = "Tag name can't be blank")]
		public string TagName { get; set; }

		/// <summary>
		/// Converts DTO object to Tag entity
		/// </summary>
		/// <returns>Tag object with DTO data</returns>
		public Tag ToTag()
		{
			return new Tag() { Name = TagName };
		}
	}
}
