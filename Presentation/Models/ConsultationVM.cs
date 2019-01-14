using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class ConsultationVM
    {
        public int id { get; set; }
        public float price { get; set; }
        public int rating { get; set; }
        //public DateTime date_cons { get; set; }
        public string remarks { get; set; }
    }
}