using System;
using System.Collections.Generic;
using System.Text;
using Abp_BeTech.Localization;
using Volo.Abp.Application.Services;

namespace Abp_BeTech;

/* Inherit your application services from this class.
 */
public abstract class Abp_BeTechAppService : ApplicationService
{
    protected Abp_BeTechAppService()
    {
        LocalizationResource = typeof(Abp_BeTechResource);
    }
}
