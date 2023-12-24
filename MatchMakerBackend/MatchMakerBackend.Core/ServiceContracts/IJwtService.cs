using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.ServiceContracts
{
	/// <summary>
	/// Interface for Jwt token related functions
	/// </summary>
	public interface IJwtService
	{
		/// <summary>
		/// Creates Jwt token
		/// </summary>
		/// <param name="user">Application user object to create token for</param>
		/// <param name="roles">List of roles the user has</param>
		/// <returns>Authentication response</returns>
		AuthenticationResponse CreateJwtToken(ApplicationUser user, IList<string> roles);
	}
}
