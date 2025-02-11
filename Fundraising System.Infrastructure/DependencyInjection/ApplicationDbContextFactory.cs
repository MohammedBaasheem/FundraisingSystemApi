using Fundraising_System.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Fundraising_System.Infrastructure.DependencyInjection
{
    //public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var configurationBuilder = new ConfigurationBuilder()
    //            .SetBasePath(Directory.GetCurrentDirectory()) // تأكد من المسار الصحيح
    //            .AddJsonFile("appsettings.json") // أو appsettings.Development.json إذا كنت تستخدمه
    //            .Build();

    //        var connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        optionsBuilder.UseSqlServer(connectionString); // استخدم سلسلة الاتصال من التكوين

    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FundraisingDB;Integrated Security=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
