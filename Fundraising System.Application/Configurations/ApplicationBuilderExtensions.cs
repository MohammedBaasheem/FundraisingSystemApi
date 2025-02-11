using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Security.Policy;

namespace Fundraising_System.Application.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder app)=> app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        public static void ConfigureSerilog(this IHostBuilder host)
        {
            host.UseSerilog((ctx, lc) =>
            lc.WriteTo.MSSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FundraisingDB;Integrated Security=True;",
            new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
                SchemaName= "dbo",
                AutoCreateSqlDatabase = true,
            }
            )
            
            );
        }
         
    }

}
