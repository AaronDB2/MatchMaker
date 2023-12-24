using MatchMakerBackend.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.Entities
{
	public class Question
	{
		[Key]
		public Guid Id { get; set; }

		// ForeignKey to challenges
		[Required]
		public Guid ChallengeId { get; set; }
		public Challenge Challenge { get; set; }

		// ForeignKey to applicationUsers
		[Required]
		public Guid UserId { get; set; }
		public ApplicationUser User { get; set; }

		[Required]
		[StringLength(320)]
		public string QuestionDescription { get; set; }

		[Required]
		[StringLength(50)]
		public string QuestionTitle { get; set; }


	}
}
