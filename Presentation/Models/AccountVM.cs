using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Presentation.Models
{

    public class AccountVM
    {

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Username")]
        public string username { get; set; }

        [StringLength(255)]
        [DataType(DataType.Password)]
        [Display(Name = "Change Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [Required]
        public string currentPassword { get; set; }
    }
}