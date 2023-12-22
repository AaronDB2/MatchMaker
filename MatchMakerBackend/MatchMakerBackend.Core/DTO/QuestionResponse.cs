using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// Question Response DTO
	/// </summary>
	public class QuestionResponse
	{
		public string? QuestionTitle { get; set; }

		public string? QuestionDescription { get; set; }

		public Guid? UserId { get; set; }

		public Guid? QuestionId { get; set; }

		public Guid? ChallengeId { get; set; }
	}
}
