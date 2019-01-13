using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class NotificationVM
    {

        public int id { get; set; }

        //public int? confirmation { get; set; }

        //public DateTime? new_Appointement_Date { get; set; }

        public DateTime? notified_at { get; set; }

        public int? patientnotif_id { get; set; }


        public string content { get; set; }

        public virtual Patient patient { get; set; }
    }
}