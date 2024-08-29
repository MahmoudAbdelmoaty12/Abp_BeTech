using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp_BeTech.Exceptions
{
    public class SepcificationNotFoundException:BusinessException
    {
        public SepcificationNotFoundException(int id) : base(Abp_BeTechDomainErrorCodes.Specification_Not_Found)
        {

            WithData("id", id);
        }
        public SepcificationNotFoundException(string Name) : base(Abp_BeTechDomainErrorCodes.Specification_Not_Found)
        {
            WithData("Name", Name);
        }
    }
}

