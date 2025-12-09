using System.Text;
using Clerk.Net.AspNetCore.Security;
using Clerk.Net.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Server;
using Server.Mapping;
using Server.Repositories;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 1. Add Clerk authentication

builder.Services.AddClerkApiClient(config =>
{
    config.SecretKey = builder.Configuration["Clerk:SecretKey"]!;
});

// 2. Add clerk JWT authentication
builder
    .Services.AddAuthentication(ClerkAuthenticationDefaults.AuthenticationScheme)
    .AddClerkAuthentication(options =>
    {
        options.Authority = builder.Configuration["Clerk:Domain"]; 
        options.AuthorizedParty = builder.Configuration["Clerk:Audience"]; 
    });

builder.Services.AddDbContext<StudyBiddyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Authentication + Authorization **MUST** be added
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference(options =>
{
    options.Theme = ScalarTheme.BluePlanet;
});

app.Run();
