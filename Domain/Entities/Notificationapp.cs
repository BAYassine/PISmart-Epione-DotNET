namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.notificationapp")]
    public partial class Notificationapp
    {
        public int id { get; set; }

        public int? confirmation { get; set; }

        public DateTime? new_Appointement_Date { get; set; }

        public DateTime? notified_at { get; set; }

        public int? patientnotif_id { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public virtual Patient patient { get; set; }
    }
}
