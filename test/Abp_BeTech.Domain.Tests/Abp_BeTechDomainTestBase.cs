using Volo.Abp.Modularity;

namespace Abp_BeTech;

/* Inherit from this class for your domain layer tests. */
public abstract class Abp_BeTechDomainTestBase<TStartupModule> : Abp_BeTechTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
