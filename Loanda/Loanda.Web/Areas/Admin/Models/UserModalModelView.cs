using System.ComponentModel.DataAnnotations;

namespace Loanda.Web.Areas.Admin.Models
{
    public class UserModalModelView
    {
        [Required]
        public string ID { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        //[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsLocked { get; set; }
    }
}