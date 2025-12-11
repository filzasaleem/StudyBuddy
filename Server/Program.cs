using System.Text;
using Clerk.Net.AspNetCore.Security;
using Clerk.Net.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Server;
using Server.Data;
using Server.Mapping;
using Server.Repositories;
using Server.Services;
using Servr.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<StudyBuddyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Clerk:Issuer"];
        options.Audience = builder.Configuration["Clerk:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Clerk:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Clerk:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            NameClaimType = "sub",
        };

        // Log token validation
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully");
                return Task.CompletedTask;
            },
        };
    });

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(
//         "AllowFrontend",
//         policy =>
//         {
//             policy
//                 .WithOrigins(
//                     "http://localhost:5173", // Vite frontend
//                     "http://localhost:3000" // Any other local frontend
//                 )
//                 .AllowAnyHeader()
//                 .AllowAnyMethod()
//                 .AllowCredentials(); // Needed for Clerk
//         }
//     );
// });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add services
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IStudyBuddyRepo, StudyBuddyRepo>();
builder.Services.AddScoped<IStudyBuddyService, StudyBuddyService>();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// app.UseCors("AllowFrontend");
app.UseCors();

// Authentication + Authorization **MUST** be added
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference(options =>
{
    options.Theme = ScalarTheme.BluePlanet;
});

app.Run();
