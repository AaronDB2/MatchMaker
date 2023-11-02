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
	/// Question repository interface for defining question repository functions
	/// </summary>
	public interface IQuestionRepository
	{
		/// <summary>
		/// Returns a list of all the questions
		/// </summary>
		/// <returns>List of questions</returns>
		Task<List<Question>?> GetAllQuestions();

		/// <summary>
		/// Returns all question objects from data store based on given expression
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>List of question objects from data store that match the given expression</returns>
		Task<List<Question>?> GetFilterdQuestions(Expression<Func<Question, bool>> predicate);

		/// <summary>
		/// Adds a new question object to the data store
		/// </summary>
		/// <param name="question">question object to add</param>
		/// <returns>The question object that was added to the data store</returns>
		Task<Question> AddQuestion(Question question);

		/// <summary>
		/// Update question data
		/// </summary>
		/// <param name="question">Question object to update</param>
		/// <returns>Updated question object</returns>
		Task<Question> UpdateQuestion(Question question);
	}
}
