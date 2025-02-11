
using Fundraising_System.Application.Configurations;
using Fundraising_System.Application.DependencyInjection;

using Fundraising_System.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Serilog.Sinks.MSSqlServer;

namespace Fundraising_System.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                
                .WriteTo.MSSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FundraisingDB;Integrated Security=True;",
            new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
                SchemaName = "dbo",
                AutoCreateSqlDatabase = true,
            }
            )
                .CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.


                builder.Host.ConfigureSerilog();


                Log.Information("Starting web host");



                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddInfrastructureService(builder.Configuration);
                //builder.Services.AddApplicationService();

                builder.Services.AddApplicationServices(builder.Configuration);


                builder.Services.AddSwaggerGen(
         swagger =>
         {
             swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Fundraising_System.Api", Version = "v1" });
             swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                 Name = "Authorization",
                 In = ParameterLocation.Header,
                 Type = SecuritySchemeType.ApiKey,
                 Scheme = "Bearer"
             });

             // ≈÷«›… ŒÌ«— ·≈œŒ«· «·ﬂÊﬂÌ
             swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] {}
            }
             });
         });








                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                app.AddGlobalErrorHandler();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();

            }
















               
        }
    }
}
