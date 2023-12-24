using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.DTO
{
	/// <summary>
	/// DTO for JWT token and auth data
	/// </summary>
	public class AuthenticationResponse
	{
		public string? Username { get; set; }

		public string? Email { get; set; }

		public string? Token { get; set; }

		public DateTime Expiration { get; set; }

		public string? RefreshToken { get; set; } = string.Empty;

		public DateTime RefreshTokenExpirationDateTime { get; set;}

	}
}
