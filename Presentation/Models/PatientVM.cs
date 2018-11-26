using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class PatientVM
    {
        public int social_number { get; set; }
        public int id { get; set; }
        public virtual ICollection<Appointment> appointments { get; set; }
        public virtual ICollection<Message> messages { get; set; }
        public virtual ICollection<Notificationapp> notificationapps { get; set; }
    }
}