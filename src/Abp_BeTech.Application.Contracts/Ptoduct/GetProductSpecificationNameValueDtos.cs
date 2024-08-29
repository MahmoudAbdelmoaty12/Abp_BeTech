using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.PtoductDto
{
    public class GetProductSpecificationNameValueDtos:EntityDto<int>
    {
        public GetAllProductsDtos productsDtos { get; set; }

        public List<GetSpecificationsNameValueDtos> specificationsNameValueDtos { get; set; }
    }
}
