using MatchMakerBackend.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.Entities
{
	public class Challenge
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string ChallengeTitle { get; set; }

		[Required]
		[StringLength(320)]
		public string ChallengeDescription { get; set;}

		// ForeignKey to companies
		[Required]
		public Guid CompanyId { get; set; }
		public Company Company { get; set; }

		[Required]
		public DateTime DateSubmitted { get; set; }

		[Required]
		[StringLength(100)]
		public string ChallengeFileName { get; set;}

		// ForeignKey to users
		[Required]
		public Guid ContactUserId { get; set; }
		public ApplicationUser ContactPerson { get; set; }

		[Required]
		[StringLength(40)]
		public string ViewStatus { get; set; }

		[StringLength(100)]
		public string? ResultFileName { get; set; }

		[Required]
		[StringLength(40)]
		public string ProgressionStatus { get; set; }

		[Required]
		public DateTime EndDate { get; set; }
	}
}
