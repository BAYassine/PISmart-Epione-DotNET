namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.profile")]
    public partial class Profile
    {

        public enum Gender { FEMALE, MALE }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profile()
        {
            users = new HashSet<User>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string address { get; set; }

        public DateTime birthDate { get; set; }

        [Required]
        [StringLength(255)]
        public string firstname { get; set; }

        public Gender gender { get; set; }

        [Required]
        [StringLength(255)]
        public string lastname { get; set; }

        [StringLength(255)]
        public string telephone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> users { get; set; }
    }
}
