using System.Threading.Tasks;

namespace Abp_BeTech.Data;

public interface IAbp_BeTechDbSchemaMigrator
{
    Task MigrateAsync();
}
