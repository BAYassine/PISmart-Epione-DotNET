namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.path")]
    public partial class Path
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Path()
        {
            treatments = new HashSet<Treatment>();
        }

        public int id { get; set; }

        public DateTime? date_path { get; set; }

         
        public string description { get; set; }

        public int? doctor_id { get; set; }

        public int? patient_id { get; set; }

        public virtual Doctor doctor { get; set; }

        public virtual Patient patient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Treatment> treatments { get; set; }
    }
}
