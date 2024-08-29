using Abp_BeTech.Samples;
using Xunit;

namespace Abp_BeTech.EntityFrameworkCore.Domains;

[Collection(Abp_BeTechTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<Abp_BeTechEntityFrameworkCoreTestModule>
{

}
