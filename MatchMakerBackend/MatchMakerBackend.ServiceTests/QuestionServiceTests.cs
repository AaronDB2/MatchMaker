using AutoFixture;
using FluentAssertions;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ServiceTests
{
	public class QuestionServiceTests
	{
		private readonly IFixture _fixture;
		private readonly IQuestionRepository _questionRepository;
		private readonly IQuestionAdderService _questionAdderService;
		private readonly IQuestionGetterService _questionGetterService;

		private readonly Mock<IQuestionRepository> _questionRepositoryMock;

		public QuestionServiceTests()
		{
			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Mock questionRepository
			_questionRepositoryMock = new Mock<IQuestionRepository>();
			_questionRepository = _questionRepositoryMock.Object;

			// Initialize services
			_questionAdderService = new QuestionAdderService(_questionRepository);
			_questionGetterService = new QuestionGetterService(_questionRepository);
		}

		[Fact]
		public async Task GetFilterdQuestions_WithCorrectQuestionId_ToBeSuccessfull()
		{
			//Arrange
			Question question1 = _fixture.Create<Question>();
			Question question2 = _fixture.Create<Question>();
			Question question3 = _fixture.Create<Question>();

			List<Question> questions = new List<Question>() { question1, question2, question3 };

			//Mock GetFilterdQuestions method from QuestionRepository 
			_questionRepositoryMock.Setup
			 (temp => temp.GetFilterdQuestions(It.IsAny<Expression<Func<Question, bool>>>()))
			 .ReturnsAsync(questions);

			//Act
			var response = await _questionGetterService.GetFilterdQuestions("QuestionId", question1.Id.ToString());

			//Assert
			response.Should().NotBeNull();
			response[0].QuestionId.Should().Be(question1.Id);
		}
	}
}
