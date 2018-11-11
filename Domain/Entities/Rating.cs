namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.rating")]
    public partial class Rating
    {
        public int id { get; set; }

        [StringLength(255)]
        public string comment { get; set; }

        public DateTime? created_at { get; set; }

        public int rate { get; set; }
    }
}
