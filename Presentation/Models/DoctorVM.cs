using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class DoctorVM
    {
        public int id { get; set; }
        public string address { get; set; }
        public string presentation { get; set; }
        public int speciality_id { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}