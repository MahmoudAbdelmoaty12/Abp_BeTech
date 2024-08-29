using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.Reviews
{
    public class ReviewDtos:FullAuditedEntityDto<int>
    {
       
       public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
