using MatchMakerBackend.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Tags repository interface for defining tags repository functions
	/// </summary>
	public interface ITagsRepository
	{
		/// <summary>
		/// Returns a list of all the tags
		/// </summary>
		/// <returns>List of tags</returns>
		Task<List<Tag>?> GetAllTags();

		/// <summary>
		/// Returns all tag objects from data store based on given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>List of tag objects from data store that match the given expression</returns>
		Task<List<Tag>?> GetFilterdTags(Expression<Func<Tag, bool>> predicate);

		/// <summary>
		/// Adds a new tag object to the data store
		/// </summary>
		/// <param name="tag">Tag object to add</param>
		/// <returns>The tag object that was added to the data store</returns>
		Task<Tag> AddTag(Tag tag);
	}
}
