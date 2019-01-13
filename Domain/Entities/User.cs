namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.user")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
         
        public string email { get; set; }

        public DateTime? last_login { get; set; }

         
        public string password { get; set; }

        public DateTime? registered_at { get; set; }

         
        public string role { get; set; }

        [Required]
         
        public string username { get; set; }

        [Column(TypeName = "bit")]
        public bool confirmed { get; set; }


        public int? profile_id { get; set; }
        public virtual Profile profile { get; set; }
    }
}
