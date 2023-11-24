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
	/// Tag entity model
	/// </summary>
	public class Tag
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(100)]
		// Must be unique
		public string Name { get; set; }

		// Join table with company entity (many to many relationship)
		public List<Company> Companies { get; set; } = new();

		// Join table with applicationUser entity (many to many relationship)
		public List<ApplicationUser> Users { get; set; } = new();

		// Join table with challenge entity (many to many relationship)
		public List<Challenge> Challenges { get; set; } = new();
	}
}
