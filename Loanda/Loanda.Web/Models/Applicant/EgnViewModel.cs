using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Loanda.Web.Models.Applicant
{
    public class EGNViewModel
    {
        [RegularExpression("([0-9]+)", ErrorMessage = "EGN must be only numbers")]
        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string EGN { get; set; }

        public long EmialId { get; set; }
    }
}
