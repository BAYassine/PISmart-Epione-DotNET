using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("epione.event")]
    public partial class Event
    {
        public int Eventid { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
