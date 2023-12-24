﻿using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Core.DTO;
using MatchMakerBackend.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Core.Services
{
	public class JwtService : IJwtService
	{
		private readonly IConfiguration _configuration;

		// Constructor
		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Creates Jwt token with auth information
		/// </summary>
		/// <param name="user">Application user to generate the token for</param>
		/// <returns>Authentication response</returns>
		public AuthenticationResponse CreateJwtToken(ApplicationUser user, IList<string> roles)
		{
			DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

			List<Claim> claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Email), //Optional
				new Claim(ClaimTypes.Name, user.UserName) // Optional
			};

			// Loop over user roles
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			SigningCredentials signingCredentials = new SigningCredentials(
				securityKey, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken tokenGenerator = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims,
				expires: expiration,
				signingCredentials: signingCredentials
				);
			
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			string token = tokenHandler.WriteToken(tokenGenerator);

			return new AuthenticationResponse() { 
				Token = token, 
				Email = user.Email, 
				Username = user.UserName, 
				Expiration = expiration, 
				RefreshToken = GenerateRefreshToken(),
				RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"])),
			};
		}

		/// <summary>
		/// Creates refresh token
		/// </summary>
		/// <returns>Refresh token</returns>
		private string GenerateRefreshToken()
		{
			byte[] bytes= new byte[64];
			var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(bytes);

			return Convert.ToBase64String(bytes);
		}
	}
}
