using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abp_BeTech.Data;
using Volo.Abp.DependencyInjection;

namespace Abp_BeTech.EntityFrameworkCore;

public class EntityFrameworkCoreAbp_BeTechDbSchemaMigrator
    : IAbp_BeTechDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbp_BeTechDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the Abp_BeTechDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Abp_BeTechDbContext>()
            .Database
            .MigrateAsync();
    }
}
