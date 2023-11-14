using MatchMakerBackend.Core.Domain.IdentityEntities;
using MatchMakerBackend.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MatchMakerBackend.Core.Domain.RepositoryContracts;
using MatchMakerBackend.Infrastructure.Repositories;
using MatchMakerBackend.Core.ServiceContracts;
using MatchMakerBackend.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Adds all controllers as services without views
builder.Services.AddControllers(options =>
{
	// Authorization policy
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	options.Filters.Add(new AuthorizeFilter(policy));
});

// Add services
builder.Services.AddScoped<ITagsRepository, TagsRepository>();
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

builder.Services.AddScoped<ICompanyAdderService, CompanyAdderService>();
builder.Services.AddScoped<ICompanyGetterService, CompanyGetterService>();
builder.Services.AddScoped<ITagAdderService, TagAdderService>();
builder.Services.AddScoped<IChallengeAdderService, ChallengeAdderService>();
builder.Services.AddScoped<IChallengeGetterService, ChallengeGetterService>();
builder.Services.AddScoped<IChallengeUpdateService, ChallengeUpdateService>();
builder.Services.AddScoped<IQuestionAdderService, QuestionAdderService>();
builder.Services.AddScoped<IQuestionGetterService, QuestionGetterService>();

// Add DbContext as a service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IJwtService, JwtService>();

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


// JWT
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});

builder.Services.AddAuthorization(options =>
{

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
