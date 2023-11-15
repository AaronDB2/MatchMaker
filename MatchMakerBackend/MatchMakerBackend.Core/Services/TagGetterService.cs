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
	public class TagGetterService : ITagGetterService
	{
		private readonly ITagsRepository _tagRepository;

		// Constructor
		public TagGetterService(ITagsRepository tagRepository)
		{
			_tagRepository = tagRepository;
		}

		public async Task<List<TagResponse?>?> GetFilterdTags(string searchBy, string? searchString)
		{
			List<Tag>? tags = searchBy switch
			{
				nameof(TagResponse.TagName) =>
				 await _tagRepository.GetFilterdTags(temp =>
				 temp.Name.Contains(searchString)),


				_ => await _tagRepository.GetAllTags()
			};

			List<TagResponse> response = new List<TagResponse>();

			// Create a TagResponse for each found tag
			foreach (Tag tag in tags)
			{
				TagResponse tagResponse = new TagResponse()
				{
					TagName = tag.Name,
					Tag = tag
				};

				response.Add(tagResponse);
			}

			return response;
		}
	}
}
