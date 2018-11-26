namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.treatment")]
    public partial class Treatment
    {
        public int id { get; set; }

         
        public string description { get; set; }

         
        public string recomended_doc { get; set; }

        public int? appointment_id { get; set; }

        public int? path_id { get; set; }

        public virtual Appointment appointment { get; set; }

        public virtual Path path { get; set; }
    }
}
