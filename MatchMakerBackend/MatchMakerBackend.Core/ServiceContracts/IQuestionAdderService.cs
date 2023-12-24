using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining question adder service
	/// </summary>
	public interface IQuestionAdderService
	{
		/// <summary>
		/// Calls repository for creating a new question entity
		/// </summary>
		/// <param name="createQuestionRequest">Data to create a new question from</param>
		/// <param name="userId">User id that created the question</param>
		/// <returns>Created question</returns>
		Task<QuestionResponse> AddQuestion(CreateQuestionRequest? createQuestionRequest, Guid userId);
	}
}
