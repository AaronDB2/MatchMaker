using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for defining file related functionality
	/// </summary>
	public interface IFileService
	{
		/// <summary>
		/// Downloads a file from UploadedFiles map based on filename
		/// </summary>
		/// <param name="fileName">Name of file to download</param>
		/// <returns>The file that wil be downloaded</returns>
		FileStream Download(string fileName);

		/// <summary>
		/// Uploads file to UploadedFiles map
		/// </summary>
		/// <param name="file">File to be uploaded</param>
		/// <returns>True if upload went correct otherwise false</returns>
		Task<bool> UploadFile(IFormFile file);
	}
}
