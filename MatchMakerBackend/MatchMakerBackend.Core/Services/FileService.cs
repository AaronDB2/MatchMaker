using MatchMakerBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class FileService : IFileService
	{
		public FileStream Download(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			var stream = new FileStream(path, FileMode.Open);

			return stream;
		}

		public async Task<bool> UploadFile(IFormFile file)
		{
			// Read the file and upload it to the UploadedFiles folder
			string path = "";
			try
			{
				if (file.Length > 0)
				{
					path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}

					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				return false;
				throw new Exception("File Copy Failed", ex);
			}
		}
	}
}
