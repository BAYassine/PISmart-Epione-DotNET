using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Models
{
    public class AppointmentVM
    {
        public enum States
        {
            CANCELED, UPCOMING, ONGOING, DONE
        }
        public int id { get; set; }
        //public int reasonId { get; set; }
        //public List<SelectListItem> reasonList { get; set; }
        public DateTime? date_end { get; set; }
        public string date_start { get; set; }
        public string message { get; set; }
        public string state { get; set; }
        public int stat { get; set; }
        public int doctor_id { get; set; }
        public string title { get; set; }
        public int patient_id { get; set; }
        public int reason_id { get; set; }
        public virtual Doctor doctor { get; set; }
        public virtual Reason reason { get; set; }
        public virtual Patient patient { get; set; }

    }
}