using MatchMakerBackend.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	// DTO for creating questions
	public class CreateQuestionRequest
	{
		public string QuestionTitle { get; set; }
		public string QuestionDescription { get; set; }
		public string UserName { get; set; }
		public Guid ChallengeId { get; set; }

		/// <summary>
		/// Converts DTO object to Question entity
		/// </summary>
		/// <returns>Question object with DTO data</returns>
		public Question ToQuestion(Guid userId)
		{
			return new Question()
			{
				QuestionTitle = QuestionTitle,
				QuestionDescription = QuestionDescription,
				ChallengeId = ChallengeId,
				UserId = userId
			};
		}
	}
}
