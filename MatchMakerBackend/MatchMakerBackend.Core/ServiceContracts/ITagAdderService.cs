using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	public interface ITagAdderService
	{
		/// <summary>
		/// Calls repository for creating a new tag entity
		/// </summary>
		/// <param name="createTagRequest">Data to create a new tag from</param>
		/// <returns>Created tag</returns>
		Task<TagResponse> AddTag(CreateTagRequest? createTagRequest);
	}
}
