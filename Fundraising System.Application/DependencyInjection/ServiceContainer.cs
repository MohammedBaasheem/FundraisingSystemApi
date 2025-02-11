using Fundraising_System.Application.UseCaseImplementation;
using Fundraising_System.Application.UseCaseInterface;
using Fundraising_System.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
 namespace Fundraising_System.Application.DependencyInjection
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add AutoMapper to the services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure JWT settings
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            services.AddSingleton(jwtSettings); // Register JwtSettings as a singleton

            // Set up authentication with JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true, // Check token expiration
                    ClockSkew = TimeSpan.Zero // Adjust for clock synchronization if needed
                };
            });

            // Register the identity service
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IProjectService, ProjectService>();
            //services.AddScoped<IProjectRepository, ProjectRepository>();
            
            return services; // Return the configured services
        }
    }

    // Dedicated class for JWT settings (Best Practice)
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }

}
