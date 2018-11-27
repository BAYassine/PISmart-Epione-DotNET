using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class PatientVM
    {

        public int id { get; set; }
        public int social_number { get; set; }
        public virtual ICollection<PathVM> paths { get; set; }



    }
}