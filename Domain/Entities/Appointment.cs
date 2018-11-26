namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("epione.appointment")]
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            consultations = new HashSet<Consultation>();
            treatments = new HashSet<Treatment>();
        }

        public int id { get; set; }

        public DateTime? date_end { get; set; }

        public DateTime? date_start { get; set; }

         
        public string message { get; set; }

        public int? state { get; set; }

        public int? doctor_id{ get; set; }

        public int? patient_id { get; set; }

        public int? reason_id { get; set; }

        public virtual Doctor doctor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consultation> consultations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Treatment> treatments { get; set; }

        public virtual Reason reason { get; set; }

        public virtual Patient patient { get; set; }
      
    }
}
