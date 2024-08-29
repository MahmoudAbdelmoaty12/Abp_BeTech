using Abp_BeTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp_BeTech.Exceptionsss
{
    public class CategoryNotFoundException:BusinessException
    {
        public CategoryNotFoundException(int id) : base(Abp_BeTechDomainErrorCodes.Category_Not_Found)
        {

            WithData("id", id);
        }
        public CategoryNotFoundException(string Name ) : base(Abp_BeTechDomainErrorCodes.Category_Found)
        {
            WithData("Name",Name );
        }
    }
}
