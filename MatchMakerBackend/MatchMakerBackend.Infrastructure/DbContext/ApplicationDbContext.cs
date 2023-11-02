using MatchMakerBackend.Core.Domain.Entities;
using MatchMakerBackend.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.Infrastructure.DbContext
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		/// <summary>
		/// Constructor. Configures the options parameter of the DbContext
		/// </summary>
		/// <param name="options">Options for DbContext</param>
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		// Db sets
		public virtual DbSet<Company> Companies { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<Challenge> Challenges { get; set; }
		public virtual DbSet<Question> Questions { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Company>().ToTable("Companies");
			builder.Entity<Tag>().ToTable("Tags");
			builder.Entity<Challenge>().ToTable("Challenges");
			builder.Entity<Question>().ToTable("Questions");

			// Disables cascade delete in database for all relations
			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			// Seed data
			Guid ADMINUSER_ID = new Guid("6FBA5BE2-7F0A-496E-8090-F02C71B645D8");
			Guid ADMINROLE_ID = new Guid("D5873591-4EF5-4D3B-A570-7E8B50748BA9");

			//seed admin role
			builder.Entity<ApplicationRole>().HasData(new ApplicationRole
			{
				Name = "Admin",
				NormalizedName = "ADMIN",
				Id = ADMINROLE_ID,
				ConcurrencyStamp = ADMINROLE_ID.ToString()
			});

			//create user
			// Normalize email and securitystamp ?????????
			ApplicationUser appUser = new ApplicationUser
			{
				Id = ADMINUSER_ID,
				Email = "aarontest@gmail.com",
				EmailConfirmed = true,
				UserName = "AaronTest",
				NormalizedUserName = "AARONTEST"
			};

			//set user password
			PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
			appUser.PasswordHash = ph.HashPassword(appUser, "Test123");

			//seed user
			builder.Entity<ApplicationUser>().HasData(appUser);

			//set user role to admin
			builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
			{
				RoleId = ADMINROLE_ID,
				UserId = ADMINUSER_ID
			});
		}
	}
}
