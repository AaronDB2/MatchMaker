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
	public class QuestionAdderService : IQuestionAdderService
	{
		private readonly IQuestionRepository _questionRepository;

		// Constructor
		public QuestionAdderService(IQuestionRepository questionRepository)
		{
			_questionRepository = questionRepository;
		}

		public async Task<QuestionResponse> AddQuestion(CreateQuestionRequest? createQuestionRequest, Guid userId)
		{
			// Convert response to question
			Question question = createQuestionRequest.ToQuestion(userId);

			//generate question Id
			question.Id = Guid.NewGuid();

			// Add question to data store
			await _questionRepository.AddQuestion(question);

			// Generate QuestionResponse
			QuestionResponse questionResponse = new QuestionResponse() { QuestionTitle = question.QuestionTitle };

			return questionResponse;
		}
	}
}
