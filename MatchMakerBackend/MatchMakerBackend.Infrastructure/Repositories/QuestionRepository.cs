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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MatchMakerBackend.Infrastructure.Repositories
{
	public class QuestionRepository : IQuestionRepository
	{
		private readonly ApplicationDbContext _db;

		public QuestionRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Add given question to the data store
		/// </summary>
		/// <param name="question">Question to add to the data store</param>
		/// <returns>Added question object</returns>
		public async Task<Question> AddQuestion(Question question)
		{
			_db.Questions.Add(question);
			await _db.SaveChangesAsync();

			return question;
		}

		/// <summary>
		/// Get all questons from data store
		/// </summary>
		/// <returns>List of questions</returns>
		public async Task<List<Question>?> GetAllQuestions()
		{
			return await _db.Questions.ToListAsync();
		}

		/// <summary>
		/// Get filterd questions from data store
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>Filterd questions</returns>
		public async Task<List<Question>?> GetFilterdQuestions(Expression<Func<Question, bool>> predicate)
		{
			return await _db.Questions.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Updates entire question entity in data store
		/// </summary>
		/// <param name="question">Question to update</param>
		/// <returns>Updated question or if not found returns given question</returns>
		public async Task<Question> UpdateQuestion(Question question)
		{
			// Find matching question in db
			Question? matchingQuestion = await _db.Questions.FirstOrDefaultAsync(temp => temp.Id == question.Id);

			// If there is no matching company in db return company
			if (matchingQuestion == null) return question;

			// Update courseText and CourseFileName
			matchingQuestion.QuestionDescription = question.QuestionDescription;
			matchingQuestion.QuestionTitle = question.QuestionTitle;
			matchingQuestion.User = question.User;

			await _db.SaveChangesAsync();

			return matchingQuestion;
		}
	}
}
