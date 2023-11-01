using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
