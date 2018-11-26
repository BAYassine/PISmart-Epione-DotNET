using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public enum Roles {
        [Display(Name ="Doctor")]
        ROLE_DOCTOR,
        [Display(Name ="Patient")]
        ROLE_PATIENT
    };

    public class UserVM
    {

        public int id { get; set; }

        [Required]
         
        [EmailAddress]
        [Display(Name ="Email")]
        public string email { get; set; }

        [Required]
         
        [Display(Name = "Username")]
        public string username { get; set; }

         
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Compare("password")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-enter Password")]
        public string confirmPassword { get; set; }
        
        [Display(Name = "Account type")]
        public Roles role { get; set; }
    }
}