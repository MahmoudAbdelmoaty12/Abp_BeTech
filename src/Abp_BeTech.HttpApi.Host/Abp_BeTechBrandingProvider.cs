using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Abp_BeTech;

[Dependency(ReplaceServices = true)]
public class Abp_BeTechBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Abp_BeTech";
}
