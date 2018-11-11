namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.availability")]
    public partial class Availability
    {
        public int id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime? end_Date { get; set; }

        public DateTime? start_Date { get; set; }

        public int? doctor_id { get; set; }

        public virtual Doctor doctor { get; set; }
    }
}
