using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Models
{
    public class SpecialityVM
    {
        public List<SelectListItem> specialities { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}