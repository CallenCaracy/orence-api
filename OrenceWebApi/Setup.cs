

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrenceApi.Data;
using OrenceApi.Services;
using OrenceWebApi.Models;
using System.Text;

namespace OrenceApi.Setup;

public class Setup
{
    WebApplicationBuilder builder;

    public Setup(WebApplicationBuilder builder)
    {
        this.builder = builder;
    }
    public void SetupCors()
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });
    }
    public void GeneralApiSetup()
    {
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
    }
    public void SetupJWT()
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        Console.WriteLine(jwtSecret+ "\n");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };
            });
        var jwtSettings = new JwtSettings
        {
            Key = jwtSecret!,
            Issuer = jwtIssuer!,
            Audience = jwtAudience!
        };
        builder.Services.AddSingleton(jwtSettings);
        builder.Services.AddScoped<JwtService>();
    }
    public void SetupDbConn()
    {

        var connectionString = Environment.GetEnvironmentVariable("DB_CONN");
        Console.WriteLine(connectionString);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

}
