using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsersPlatform.Data;

namespace UsersPlatform.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UsersPlatformContext>
    {
        public UsersPlatformContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<UsersPlatformContext>();
            var connectionString = configuration.GetConnectionString("UsersPlatformContextConnection");
            builder.UseSqlServer(connectionString);
            return new UsersPlatformContext(builder.Options);
        }
    }
}
