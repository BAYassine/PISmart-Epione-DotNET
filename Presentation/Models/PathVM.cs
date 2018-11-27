using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class PathVM
    {
        public int id { get; set; }
       
        public string description { get; set; }
        public string date_path { get; set; }
        public virtual PatientVM patient { get; set; }
       // public virtual ICollection<TreatmentVM> treatments { get; set; }

        //public Boolean test { get; set; }


        // public DateTime? date_path { get; set; }

    }
}