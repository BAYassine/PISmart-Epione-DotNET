using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class AppointmentVM
    {
        public int id { get; set; }
        public DateTime? date_end { get; set; }
        public DateTime? date_start { get; set; }   
        public string message { get; set; }
        public string sujet { get; set; }
        public Appointment.States state { get; set; }
        public int? doctor_id { get; set; }
        public int? patient_id { get; set; }
        public int? reason_id { get; set; }
  //      public virtual Doctor doctor { get; set; }
    //    public virtual Reason reason { get; set; }
      //  public virtual Patient patient { get; set; }
    }
}