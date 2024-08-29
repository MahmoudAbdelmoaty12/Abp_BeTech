using Volo.Abp.Modularity;

namespace Abp_BeTech;

[DependsOn(
    typeof(Abp_BeTechApplicationModule),
    typeof(Abp_BeTechDomainTestModule)
)]
public class Abp_BeTechApplicationTestModule : AbpModule
{

}
