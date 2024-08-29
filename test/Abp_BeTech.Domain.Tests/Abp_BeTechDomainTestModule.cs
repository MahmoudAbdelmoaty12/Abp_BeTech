using Volo.Abp.Modularity;

namespace Abp_BeTech;

[DependsOn(
    typeof(Abp_BeTechDomainModule),
    typeof(Abp_BeTechTestBaseModule)
)]
public class Abp_BeTechDomainTestModule : AbpModule
{

}
