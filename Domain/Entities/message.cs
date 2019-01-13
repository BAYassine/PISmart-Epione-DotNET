namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.message")]
    public partial class Message
    {
        public int id { get; set; }

         
        public string content { get; set; }

        public DateTime? date_msg { get; set; }

        [Column(TypeName = "bit")]
        public bool seen { get; set; }

        public int? doctor_id { get; set; }

        public int? patient_id { get; set; }

        public virtual Doctor doctor { get; set; }

        public virtual Patient patient { get; set; }
    }
}
