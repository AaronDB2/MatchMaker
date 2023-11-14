using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining question getter service
	/// </summary>
	public interface IQuestionGetterService
	{
		/// <summary>
		/// Gets the questions that matches the search criteria
		/// </summary>
		/// <param name="searchBy">Value to search by</param>
		/// <param name="searchString">Search value</param>
		/// <returns>List of questions as QuestionResponse that matched the search criteria</returns>
		Task<List<QuestionResponse>?> GetFilterdQuestions(string searchBy, string? searchString);
	}
}
