using Fundraising_System.Application.DependencyInjection;
using Fundraising_System.Application.UseCaseImplementation;
using Fundraising_System.Application.UseCaseInterface;
using Fundraising_System.Domain.Entities;
using Fundraising_System.Domain.RepositoryInterfaces;
using Fundraising_System.Infrastructure.DatabaseContext;
using Fundraising_System.Infrastructure.RepositoryImplementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Fundraising_System.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserUseCase, UserUseCase>();
            //services.AddScoped<JWT>();
            services.AddScoped<IIdentityRepository,IdentityRepository>();
            services.AddScoped<IIdentityService, IdentityService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configuration of JWT settings
            //var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>(); // Use a dedicated class for JWT settings
            services.Configure<JwtSettings>(configuration.GetSection("JWT")); // Configure JwtSettings using Configure

           //**// services.AddSingleton(jwtSettings); // Register the JwtSettings as a singleton

            services.AddScoped<JwtSettings>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IDonationService, DonationService>();
            
            services.AddScoped<IProjectService, ProjectService>();
            return services;
        }
    }
}
