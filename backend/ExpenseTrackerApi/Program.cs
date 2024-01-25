using System.Text;
using System.Threading.RateLimiting;
using Domain.Interfaces;
using Domain.Models;
using Domain.Validators;
using ExpenseTrackerApi.Handlers;
using ExpenseTrackerApi.Middlewares;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Service;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// disabled
// builder.Services.AddHttpLogging(o => { });
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policy => { policy.WithOrigins("https://example.com"); });
});

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

// Policies
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("BlogOwnerPolicy", policy =>
	{
		policy.Requirements.Add(new BlogAuthorizationRequirement());
	});
	options.AddPolicy("ExpenseOwnerPolicy", policy =>
	{
		policy.Requirements.Add(new ExpenseAuthorizationRequirement());
	});
	options.AddPolicy("IncomeOwnerPolicy", policy =>
	{
		// policy.Requirements.Add(new IncomeAuthorizationRequirement());
	});
});

builder.Services.AddScoped<IAuthorizationHandler, BlogAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ExpenseAuthorizationHandler>();

var connectionString = configuration["DefaultConnection"];
if (connectionString == null) throw new ArgumentNullException(nameof(connectionString), "DefaultConnection is null");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
	options.UseMySql(
		connectionString,
		new MySqlServerVersion(new Version(8, 0, 35)),
		b => b.MigrationsAssembly("ExpenseTrackerApi")
	);
});

builder.Services.AddAuthentication();

// important for adding routes based on controllers
builder.Services.AddControllers().AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

// validators
builder.Services.AddScoped<IValidator<Blog>, BlogValidator>();
builder.Services.AddScoped<IValidator<ExpenseGroup>, ExpenseGroupValidator>();
builder.Services.AddScoped<IValidator<Expense>, ExpenseValidator>();
builder.Services.AddScoped<IValidator<IncomeGroup>, IncomeGroupValidator>();
builder.Services.AddScoped<IValidator<Income>, IncomeValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();

// services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<GetCurrentUserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<GetAuthenticatedUserIdService>();
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<IncomeService>();
builder.Services.AddScoped<ReminderService>();
builder.Services.AddScoped<ExpenseGroupService>();
builder.Services.AddScoped<IncomeGroupService>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var validIssuer = configuration["Jwt:Issuer"] ?? "https://joydipkanjilal.com/";
	var validAudience = configuration["Jwt:Audience"] ?? "https://joydipkanjilal.com/";
	var issuerSigningKey = configuration["Jwt:Key"] ??
						   "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.	eyJkZGFzYWRoYXNiZCBhc2RhZHMgc2Rhc3AgZGFzIGRhc2RhcyBhc2RhcyBkYXNkIGFzZGFzZGFzZCBhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzIGFzIGRhcyBkYXNhZGFzZGFzZCBhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGFzIGRhcyBkYXNhIGRhcyBkYXNhZGFzIGRhcyBkYXNhZGphcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhIGRhcyBkYXNhZGphcyIsImlhdCI6MTYzNDEwNTUyMn0.S7G4f8pW7sGJ7t9PIShNElA0RRve-HlPfZRvX8hnZ6c";

	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = validIssuer,
		ValidAudience = validAudience,

		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = false,
		ValidateIssuerSigningKey = true
	};
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Expense Tracker", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."

	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
						  {
							  Reference = new OpenApiReference
							  {
								  Type = ReferenceType.SecurityScheme,
								  Id = "Bearer"
							  }
						  },
						 Array.Empty<string>()
					}
				});
});

builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
	.AddFixedWindowLimiter("fixed", options =>
	{
		options.PermitLimit = 4;
		options.Window = TimeSpan.FromSeconds(12);
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 2;
	}));

var app = builder.Build();

// app.UseHttpLogging();
app.UseRateLimiter();

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.MapControllers();

// auth
app.UseAuthentication();
app.UseAuthorization();

// this middleware needs to be after .net auth middlewares!
app.UseMiddleware<ClaimsMiddleware>();

// cors
app.UseCors();

app.Run();