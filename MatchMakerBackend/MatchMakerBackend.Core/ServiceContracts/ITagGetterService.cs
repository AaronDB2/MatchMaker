using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining tag getter service 
	/// </summary>
	public interface ITagGetterService
	{
		/// <summary>
		/// Gets the tags that matches the search criteria
		/// </summary>
		/// <param name="searchBy">Value to search by</param>
		/// <param name="searchString">Search value</param>
		/// <returns>List of tags as TagResponse that matched the search criteria</returns>
		Task<List<TagResponse?>?> GetFilterdTags(string searchBy, string? searchString);
	}
}
