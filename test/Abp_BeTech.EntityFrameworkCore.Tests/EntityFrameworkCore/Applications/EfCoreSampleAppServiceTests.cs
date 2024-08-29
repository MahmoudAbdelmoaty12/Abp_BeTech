using Abp_BeTech.Samples;
using Xunit;

namespace Abp_BeTech.EntityFrameworkCore.Applications;

[Collection(Abp_BeTechTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<Abp_BeTechEntityFrameworkCoreTestModule>
{

}
