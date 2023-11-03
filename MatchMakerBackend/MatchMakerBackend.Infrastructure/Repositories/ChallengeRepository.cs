﻿using Azure;
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

namespace MatchMakerBackend.Infrastructure.Repositories
{
	/// <summary>
	/// Challenge repository inherits from IChallengeRepository
	/// </summary>
	public class ChallengeRepository : IChallengeRepository
	{
		private readonly ApplicationDbContext _db;

		public ChallengeRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Add given challenge to the data store
		/// </summary>
		/// <param name="challenge">Challenge to add to the data store</param>
		/// <returns>Added challenge object</returns>
		public async Task<Challenge> AddChallenge(Challenge challenge)
		{
			_db.Challenges.Add(challenge);
			await _db.SaveChangesAsync();

			return challenge;
		}

		/// <summary>
		/// Get all challenges from data store
		/// </summary>
		/// <returns>List of challenges</returns>
		public async Task<List<Challenge>?> GetAllChallenges()
		{
			return await _db.Challenges.ToListAsync();
		}

		/// <summary>
		/// Get filterd challenges from data store
		/// </summary>
		/// <param name="predicate">LINQ expression to check</param>
		/// <returns>Filterd challenges</returns>
		public async Task<List<Challenge>?> GetFilterdChallenges(Expression<Func<Challenge, bool>> predicate)
		{
			return await _db.Challenges.Where(predicate).ToListAsync();
		}

		/// <summary>
		/// Updates entire challenge entity in data store
		/// </summary>
		/// <param name="challenge">Challenge to update</param>
		/// <returns>Updated challenge or if not found returns given challenge</returns>
		public async Task<Challenge> UpdateChallenge(Challenge challenge)
		{
			// Find matching challenge in db
			Challenge? matchingChallenge = await _db.Challenges.FirstOrDefaultAsync(temp => temp.Id == challenge.Id);

			// If there is no matching challenge in db return challenge
			if (matchingChallenge == null) return challenge;

			// Update courseText and CourseFileName
			matchingChallenge = challenge;

			await _db.SaveChangesAsync();

			return matchingChallenge;
		}
	}
}