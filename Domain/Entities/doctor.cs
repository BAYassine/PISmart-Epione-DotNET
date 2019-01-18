namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.doctor")]
    public partial class Doctor : User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            appointments = new HashSet<Appointment>();
            availabilities = new HashSet<Availability>();
            paths = new HashSet<Path>();
            messages = new HashSet<Message>();
            reasons = new HashSet<Reason>();
        }


        [StringLength(255)]
        public string presentation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? speciality_id { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string adresseSocialSiege { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string formeJuridique { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        [StringLength(255)]
        public string memberAGA { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string nbreInscriptionOrdre { get; set; }

        [StringLength(255)]
        public string nbreRCS { get; set; }

        [StringLength(255)]
        public string nbreRPPS { get; set; }

        [StringLength(255)]
        public string socialReason { get; set; }

        [StringLength(255)]
        public string statuts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> appointments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Availability> availabilities { get; set; }

        public virtual Speciality speciality { get; set; }

        public virtual Skill skill { get; set; }

        public virtual User user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Path> paths { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reason> reasons { get; set; }
    }
}
