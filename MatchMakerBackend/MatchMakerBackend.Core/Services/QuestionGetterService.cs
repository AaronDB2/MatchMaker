using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class QuestionGetterService : IQuestionGetterService
	{
		private readonly IQuestionRepository _questionRepository;

		// Constructor
		public QuestionGetterService(IQuestionRepository questionRepository)
		{
			_questionRepository = questionRepository;
		}

		public async Task<List<QuestionResponse>?> GetFilterdQuestions(string searchBy, string? searchString)
		{
			List<Question>? questions = searchBy switch
			{
				nameof(QuestionResponse.QuestionId) =>
				 await _questionRepository.GetFilterdQuestions(temp =>
				 temp.Id.ToString().Contains(searchString)),

				_ => await _questionRepository.GetAllQuestions()
			};

			List<QuestionResponse> response = new List<QuestionResponse>();

			// Create a QuestionResponse for each found question
			foreach (Question question in questions)
			{
				QuestionResponse questionResponse = new QuestionResponse()
				{
					QuestionTitle = question.QuestionTitle,
					QuestionId = question.Id,
					QuestionDescription = question.QuestionDescription,
					UserId = question.UserId
				};

				response.Add(questionResponse);
			}

			return response;
		}
	}
}
