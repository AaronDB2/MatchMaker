using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MatchMakerBackend.IntergrationTests
{
	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);

			//builder.UseEnvironment("Test");

			builder.ConfigureServices(services => {
				var descripter = services.SingleOrDefault(temp => temp.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

				if (descripter != null)
				{
					services.Remove(descripter);
				}

				services.AddDbContext<ApplicationDbContext>(options =>
				{
					options.UseInMemoryDatabase("DatabaseForTesting");
				});

				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<ApplicationDbContext>();

					db.Database.EnsureDeleted();

					db.Database.EnsureCreated();

					// Call database seeder
					TestDBSeeder.InitializeDbForTests(db);
				}
			});
		}
	}
}
