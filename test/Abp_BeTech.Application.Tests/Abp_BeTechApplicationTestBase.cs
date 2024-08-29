using Volo.Abp.Modularity;

namespace Abp_BeTech;

public abstract class Abp_BeTechApplicationTestBase<TStartupModule> : Abp_BeTechTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
