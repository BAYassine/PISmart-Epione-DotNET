namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.report")]
    public partial class Report
    {
        public int id { get; set; }

         
        public string content { get; set; }

        public DateTime? date_rep { get; set; }

        public int? consultation_id { get; set; }

         
        public string pathFile { get; set; }

        public int? patient_id { get; set; }

        public virtual Consultation consultation { get; set; }

        public virtual Patient patient { get; set; }
    }
}
