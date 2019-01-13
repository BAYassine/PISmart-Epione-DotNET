using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class AppointmentVM
    {
        public enum States
        {
            CANCELED, UPCOMING, ONGOING, DONE
        }
        public int id { get; set; }

        public DateTime? date_end { get; set; }

        public DateTime? date_start { get; set; }

        public string message { get; set; }

        public States state { get; set; }

        public int? doctor_id { get; set; }

        public int? patient_id { get; set; }

        public int? reason_id { get; set; }
    }
}