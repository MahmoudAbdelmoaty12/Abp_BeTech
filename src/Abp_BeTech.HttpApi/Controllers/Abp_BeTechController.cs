using Abp_BeTech.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp_BeTech.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Abp_BeTechController : AbpControllerBase
{
    protected Abp_BeTechController()
    {
        LocalizationResource = typeof(Abp_BeTechResource);
    }
}
