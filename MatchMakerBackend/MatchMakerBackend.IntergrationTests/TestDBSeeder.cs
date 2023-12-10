using AutoFixture;
using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.IntergrationTests
{
	public class TestDBSeeder
	{
		/// <summary>
		/// Seeds the test database for intergration tests
		/// </summary>
		/// <param name="context">DB Context</param>
		public static void InitializeDbForTests(ApplicationDbContext context)
		{
			IFixture fixture = new Fixture();

			// client has a circular reference from AutoFixture point of view
			fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			var data = fixture.Create<Challenge>();

			data.Id = new Guid("68977C78-A753-463E-ACE3-BA20ED1E5D6E");

			context.Challenges.Add(data);
			context.SaveChanges();
		}
	}
}
