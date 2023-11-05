using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class TagAdderService : ITagAdderService
	{
		private readonly ITagsRepository _tagsRepository;

		/// <summary>
		/// Constructor
		/// </summary>
		public TagAdderService(ITagsRepository tagsRepository) 
		{
			_tagsRepository = tagsRepository;
		}

		public async Task<TagResponse> AddTag(CreateTagRequest? createTagRequest)
		{
			// Convert response to tag
			Tag tag = createTagRequest.ToTag();

			//generate tag Id
			tag.Id = Guid.NewGuid();

			// Add tag to data store
			await _tagsRepository.AddTag(tag);

			// Generate TagResponse
			TagResponse tagResponse = new TagResponse() { TagName = tag.Name };

			return tagResponse;
		}
	}
}
