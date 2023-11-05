using AutoFixture;
using FluentAssertions;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.ServiceTests
{
	public class TagServiceTests
	{
		private readonly IFixture _fixture;
		private readonly ITagsRepository _tagRepository;
		private readonly ITagAdderService _tagAdderService;

		private readonly Mock<ITagsRepository> _tagRepositoryMock;

		public TagServiceTests()
		{
			// Mock
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Mock tagRepository
			_tagRepositoryMock = new Mock<ITagsRepository>();
			_tagRepository = _tagRepositoryMock.Object;

			// Initialize services
			_tagAdderService = new TagAdderService(_tagRepository);
		}

		[Fact]
		public async Task AddTag_TagDetailsComplete_ToBeSuccessfull()
		{
			//Arrange
			CreateTagRequest createTagRequest = _fixture.Create<CreateTagRequest>();
			Tag tag = createTagRequest.ToTag();
			TagResponse tagResponse = new TagResponse()
			{
				TagName = tag.Name
			};

			//Mock AddTag method from TagRepository 
			_tagRepositoryMock.Setup
			 (temp => temp.AddTag(It.IsAny<Tag>()))
			 .ReturnsAsync(tag);

			//Act
			TagResponse result = await _tagAdderService.AddTag(createTagRequest);

			//Assert
			result.TagName.Should().Be(tagResponse.TagName);

		}
	}
}
