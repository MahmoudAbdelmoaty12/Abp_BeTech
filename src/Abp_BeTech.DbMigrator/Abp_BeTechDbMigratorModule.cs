using Abp_BeTech.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp_BeTech.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Abp_BeTechEntityFrameworkCoreModule),
    typeof(Abp_BeTechApplicationContractsModule)
    )]
public class Abp_BeTechDbMigratorModule : AbpModule
{
}
