using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class GetSpecificationsNameValueDtos:FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
