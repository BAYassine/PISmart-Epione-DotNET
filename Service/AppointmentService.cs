using Epione.Data.Infrastructures;
using Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class AppointmentService : Service<Appointment>, IAppointmentService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public AppointmentService() : base(utk)
        {
        }
        public int CountCanceledAppointment(int id)
        {
            return GetMany(c => c.patient.Id==id).Count();
        }
    }
}
