using MatchMakerBackend.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.IdentityEntities
{
	/// <summary>
	/// User entity model
	/// </summary>
	public class ApplicationUser : IdentityUser<Guid>
	{
		// ForeignKey to companies
		public Guid CompanyId { get; set; }
		public Company Company { get; set; }

		// Join table with tag entity (many to many relationship)
		public List<Tag> Tags { get; set; } = new();
	}
}
