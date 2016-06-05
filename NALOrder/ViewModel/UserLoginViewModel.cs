using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NALOrder.ViewModel
{
    public class UserLoginViewModel
    {
        [Required]
        //[EmailAddress]
        [StringLength(150, MinimumLength = 3)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password ")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}