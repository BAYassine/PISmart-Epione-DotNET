namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.profile")]
    public partial class Profile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profile()
        {
            users = new HashSet<User>();
        }

        public int id { get; set; }

        [Required]
         
        public string address { get; set; }

        public DateTime birthDate { get; set; }

        [Required]
         
        public string firstname { get; set; }

        public int gender { get; set; }

        [Required]
         
        public string lastname { get; set; }

         
        public string telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> users { get; set; }
    }
}
