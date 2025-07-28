using ChronoTrack.Model.DTOs.Auth;
using ChronoTrack.Repository.Data;
using ChronoTrack.Repository.Entities;
using ChronoTrack.Repository.Interfaces;
using ChronoTrack.Repository.Repositories;
using ChronoTrack.Service.Interfaces;
using ChronoTrack.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ChronoTrack API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Database
builder.Services.AddDbContext<ChronoTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// === Custom Authentication Services ===
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
})
.AddOpenIdConnect("AzureAD", options =>
{
    var azureAd = builder.Configuration.GetSection("Authentication:AzureAd");
    options.Authority = $"https://login.microsoftonline.com/{azureAd["TenantId"]}/v2.0";
    options.ClientId = azureAd["ClientId"];
    options.ClientSecret = azureAd["ClientSecret"];
    options.ResponseType = "code";
    options.CallbackPath = "/signin-oidc";
    options.SaveTokens = true;
    options.Scope.Add("email");
    options.Scope.Add("profile");

    options.Events.OnTokenValidated = async context =>
    {
        var email = context.Principal.FindFirst("preferred_username")?.Value
                    ?? context.Principal.FindFirst("email")?.Value;

        if (email != null)
        {
            var firstName = context.Principal.FindFirst("given_name")?.Value ?? "Unknown";
            var lastName = context.Principal.FindFirst("family_name")?.Value ?? "User";

            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
            var response = await authService.HandleExternalLoginAsync(email, firstName, lastName, LoginType.AzureAd);

            // Option A: redirect with token
            var token = response.Token;
            var refresh = response.RefreshToken;
            context.Response.Redirect($"https://localhost:7097/api/auth/token-received?token={token}&refresh={refresh}");
            context.HandleResponse();
        }
    };

});



// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
