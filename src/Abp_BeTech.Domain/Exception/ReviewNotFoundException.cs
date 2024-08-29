using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp_BeTech.Exceptionv
{
    public class ReviewNotFoundException:BusinessException
    {
       
       
            public ReviewNotFoundException(int id) : base(Abp_BeTechDomainErrorCodes.Review_Not_Found)
            {

                WithData("id", id);
            }
            public ReviewNotFoundException(string Name) : base("Review Found")
            {
                WithData("Name", Name);
            }
        }
    
}
