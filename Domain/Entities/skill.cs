namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("epione.skills")]
    public partial class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Doctor_id { get; set; }

         
        public string skills { get; set; }

        public virtual Doctor doctor { get; set; }
    }
}
