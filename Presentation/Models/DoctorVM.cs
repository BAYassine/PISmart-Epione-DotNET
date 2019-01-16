using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Models
{
    public class DoctorVM
    {

         
        public string presentation { get; set; }

        public int id { get; set; }
        public int speciality_id { get; set; }
        public virtual Speciality speciality { get; set; }
       // public string speciality { get; set; }
        public List<SelectListItem> specialites { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string image { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public List<SelectListItem> listreasons { get; set; }
        public virtual ICollection<Reason> reasons { get; set; }
        public string memberAGA { get; set; }

         
        public string name { get; set; }

         
        public string nbreInscriptionOrdre { get; set; }


    }


}