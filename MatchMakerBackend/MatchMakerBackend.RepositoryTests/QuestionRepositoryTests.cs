using AutoFixture;
using EntityFrameworkCoreMock;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using MatchMakerBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.RepositoryTests
{
	public class QuestionRepositoryTests
	{
		private readonly IQuestionRepository _questionRepository;

		private readonly IFixture _fixture;

		// Test data
		private List<Question> questions = new List<Question>();

		Question testQuestion;

		// constructor
		public QuestionRepositoryTests()
		{
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Generate test data
			for (int i = 0; i < 10; i++)
			{
				questions.Add(_fixture.Create<Question>());
			}

			testQuestion = questions[0];

			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Questions, questions);

			_questionRepository = new QuestionRepository(dbContext);
		}

		[Fact]
		public async Task GetAllQuestions_ShouldGetAllQuestionsFromDataStore()
		{
			// Act
			List<Question> Result = await _questionRepository.GetAllQuestions();

			// Assert
			Assert.NotEmpty(Result);
			Assert.Equal(10, Result.Count);
			Assert.Equal(Result[1].Id, questions[1].Id);
		}

		[Fact]
		public async Task GetFilterdQuestions_ShouldGetFilterdQuestionsBasedOnExpression()
		{
			// Act
			List<Question> returnedQuestions = await _questionRepository.GetFilterdQuestions(temp => temp.QuestionTitle.Contains(questions[7].QuestionTitle));

			// Assert
			Assert.NotEmpty(returnedQuestions);
			Assert.Equal(questions[7].QuestionTitle, returnedQuestions[0].QuestionTitle);
		}

		[Fact]
		public async Task AddQuestion_ShouldAddGivenQuestion()
		{
			// Arrange
			Question newQuestion = _fixture.Create<Question>();

			// Act
			Question result = await _questionRepository.AddQuestion(newQuestion);

			// Assert
			Assert.Equal(result, newQuestion);
		}

		[Fact]
		public async Task UpdateQuestion_ShouldUpdateQuestionIfExistsInDataStore()
		{
			// Arrange
			Question updateQuestion = _fixture.Create<Question>();
			updateQuestion.Id = testQuestion.Id;

			// Act
			Question result = await _questionRepository.UpdateQuestion(updateQuestion);

			// Assert
			Assert.Equal(result.QuestionTitle, updateQuestion.QuestionTitle);
		}
	}
}
