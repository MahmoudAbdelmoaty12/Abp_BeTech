using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp_BeTech.Data;

/* This is used if database provider does't define
 * IAbp_BeTechDbSchemaMigrator implementation.
 */
public class NullAbp_BeTechDbSchemaMigrator : IAbp_BeTechDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
