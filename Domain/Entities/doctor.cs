namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.doctor")]
    public partial class Doctor
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

         
        public string location { get; set; }

         
        public string presentation { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? speciality_id { get; set; }

         
        public string address { get; set; }

         
        public string adresseSocialSiege { get; set; }

         
        public string city { get; set; }

         
        public string formeJuridique { get; set; }

         
        public string image { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

         
        public string memberAGA { get; set; }

         
        public string name { get; set; }

         
        public string nbreInscriptionOrdre { get; set; }

         
        public string nbreRCS { get; set; }

         
        public string nbreRPPS { get; set; }

         
        public string socialReason { get; set; }

         
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
