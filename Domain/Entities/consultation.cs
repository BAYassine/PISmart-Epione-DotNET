namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.consultation")]
    public partial class Consultation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consultation()
        {
            reports = new HashSet<Report>();
        }

        public int id { get; set; }

        public DateTime? date_cons { get; set; }

        public double price { get; set; }

        public int rating { get; set; }

        [StringLength(255)]
        public string remarks { get; set; }

        public int? appointment_id { get; set; }

        public virtual Appointment appointment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> reports { get; set; }
    }
}
