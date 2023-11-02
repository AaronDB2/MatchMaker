using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adds all controllers as services without views
builder.Services.AddControllers();

// Add services
builder.Services.AddScoped<ITagsRepository, TagsRepository>();
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();

// Add DbContext as a service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Configure Identity
builder.Services
	.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders()
	.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
	.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

// Configure authorization rules
builder.Services.AddAuthorization(options =>
{
	options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

var app = builder.Build();

// create application pipeline
//app.UseStaticFiles(); // Middleware for serving static files
app.UseRouting(); // Middleware for routing
app.UseAuthentication(); // Middleware for reading authentication cookie
app.UseAuthorization(); // Middleware for authorization. Validates access permissions of the user
app.MapControllers(); // Middleware for Executing filter pipeline

app.Run();
