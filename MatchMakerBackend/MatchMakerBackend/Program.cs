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
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

// Add DbContext as a service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure authorization rules
//builder.Services.AddAuthorization(options =>
//{
//	options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
//});

// Configure identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
	options.Password.RequiredLength = 5;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = true;
	options.Password.RequireDigit = true;
})
 .AddEntityFrameworkStores<ApplicationDbContext>()
 .AddDefaultTokenProviders()
 .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
 .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
 ;

// Configure CORS
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder => {
		builder.WithOrigins("http://localhost:9500").AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();

// create application pipeline
//app.UseHsts();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseRouting(); // Middleware for routing
app.UseCors(); // Middleware for enabling CORS
app.UseAuthentication(); // Middleware for reading authentication cookie
app.UseAuthorization(); // Middleware for authorization. Validates access permissions of the user
app.MapControllers(); // Middleware for Executing filter pipeline

app.Run();
