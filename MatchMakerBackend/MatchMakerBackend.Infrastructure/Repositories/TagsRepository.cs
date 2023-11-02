using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Infrastructure.Repositories
{
	/// <summary>
	/// Tags repository inherits from ITagsRepository
	/// </summary>
	public class TagsRepository : ITagsRepository
	{
		private readonly ApplicationDbContext _db;

		public TagsRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Add given tag to the data store
		/// </summary>
		/// <param name="tag">Tag to add to the data store</param>
		/// <returns>Added tag object</returns>
		public async Task<Tag> AddTag(Tag tag)
		{
			_db.Tags.Add(tag);
			await _db.SaveChangesAsync();

			return tag;
		}

		/// <summary>
		/// Get all tags from data store
		/// </summary>
		/// <returns>List of tags</returns>
		public async Task<List<Tag>?> GetAllTags()
		{
			return await _db.Tags.ToListAsync();
		}

		/// <summary>
		/// Get filterd tags from data store
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>Filterd tags</returns>
		public async Task<List<Tag>?> GetFilterdTags(Expression<Func<Tag, bool>> predicate)
		{
			return await _db.Tags.Where(predicate).ToListAsync();
		}
	}
}
