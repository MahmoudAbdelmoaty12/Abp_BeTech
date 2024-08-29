using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abp_BeTech.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class Abp_BeTechDbContextFactory : IDesignTimeDbContextFactory<Abp_BeTechDbContext>
{
    public Abp_BeTechDbContext CreateDbContext(string[] args)
    {
        Abp_BeTechEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<Abp_BeTechDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new Abp_BeTechDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Abp_BeTech.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
