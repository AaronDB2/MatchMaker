using AutoFixture;
using EntityFrameworkCoreMock;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Infrastructure.DbContext;
using MatchMakerBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.RepositoryTests
{
	public class TagsRepositoryTests
	{
		private readonly TagsRepository _tagsRepository;

		private readonly IFixture _fixture;

		// Test data
		private List<Tag> tags = new List<Tag>();

		// constructor
		public TagsRepositoryTests()
		{
			_fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			_fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			// Generate test data
			for(int i = 0; i < 10; i++)
			{
				tags.Add(_fixture.Create<Tag>());
			}

			// Mock the dbContext
			DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
			var dbContext = dbContextMock.Object;

			// Mock db sets
			dbContextMock.CreateDbSetMock(temp => temp.Tags, tags);

			_tagsRepository = new TagsRepository(dbContext);
		}

		[Fact]
		public async Task GetAllTags_ShouldGetAllTagsFromDataStore()
		{
			// Act
			List<Tag> Result = await _tagsRepository.GetAllTags();

			// Assert
			Assert.NotEmpty(Result);
			Assert.Equal(10, Result.Count);
			Assert.Equal(Result[1].Id, tags[1].Id);
		}

		[Fact]
		public async Task GetFilterdTags_ShouldGetFilterdTagsBasedOnExpression()
		{
			// Act
			List<Tag> returnedTags = await _tagsRepository.GetFilterdTags(temp => temp.Name.Contains(tags[7].Name));

			// Assert
			Assert.NotEmpty(returnedTags);
			Assert.Equal(tags[7].Name, returnedTags[0].Name);
		}

		[Fact]
		public async Task AddTag_ShouldAddGivenTag()
		{
			// Arrange
			Tag newTag = _fixture.Create<Tag>();

			// Act
			Tag result = await _tagsRepository.AddTag(newTag);

			// Assert
			Assert.Equal(result, newTag);
		}



	}
}
