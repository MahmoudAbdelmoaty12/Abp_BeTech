using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp_BeTech.ReviewDto
{
    public class CreateUpdateReviewDto:EntityDto<int>
    {

     //   public Guid UserId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Rating is Required")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is Required")]
        public string Comment { get; set; }
   
    }
}
