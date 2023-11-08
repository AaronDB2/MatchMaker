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
	/// Challenge repository interface for defining challenge repository functions
	/// </summary>
	public interface IChallengeRepository
	{
		/// <summary>
		/// Returns a list of all the challenges
		/// </summary>
		/// <returns>List of challenges</returns>
		Task<List<Challenge>?> GetAllChallenges();

		/// <summary>
		/// Gets challenge that matches the given Id
		/// </summary>
		/// <param name="id">Challenge Id to get</param>
		/// <returns>Challenge that matches the Id</returns>
		Task<Challenge?> GetChallengeById(Guid id);

		/// <summary>
		/// Returns all challenge objects from data store based on given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>List of challenge objects from data store that match the given expression</returns>
		Task<List<Challenge>?> GetFilterdChallenges(Expression<Func<Challenge, bool>> predicate);

		/// <summary>
		/// Adds a new challenge object to the data store
		/// </summary>
		/// <param name="challenge">challenge object to add</param>
		/// <returns>The challenge object that was added to the data store</returns>
		Task<Challenge> AddChallenge(Challenge challenge);

		/// <summary>
		/// Update challenge data
		/// </summary>
		/// <param name="challenge">Challenge object to update</param>
		/// <returns>Updated challenge object</returns>
		Task<Challenge> UpdateChallenge(Challenge challenge);
	}
}
