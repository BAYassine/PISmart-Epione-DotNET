using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class TreatmentVM
    {
        public int id { get; set; }
        public String description { get; set; }
        public String recomended_doc { get; set; }
        public virtual PathVM path { get; set; }

        //  public String recomended_doc { get; set; }

    }
}