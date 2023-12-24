using MatchMakerBackend.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.Entities
{
	/// <summary>
	/// Company entity model
	/// </summary>
	public class Company
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		// Needs to be unique
		public string CompanyName { get; set; }

		[StringLength(500)]
		public string? CompanyDescription { get; set; }

		// Company can have many users
		public ICollection<ApplicationUser> Users { get; }

		// Join table with tag entity (many to many relationship)
		public List<Tag> Tags { get; set; } = new();
	}
}
