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
        [StringLength(255)]
        public string email { get; set; }

        public DateTime? last_login { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        public DateTime? registered_at { get; set; }

        [StringLength(255)]
        public string role { get; set; }

        [Required]
        [StringLength(255)]
        public string username { get; set; }

        [Column(TypeName = "bit")]
        public bool confirmed { get; set; }


        public int? profile_id { get; set; }
        public virtual Profile profile { get; set; }
    }
}
