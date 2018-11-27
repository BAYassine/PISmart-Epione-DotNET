using Domain.Entities;
using Epione.Data.Infrastructures;
using Service.Pattern;

namespace Service
{
    public class DoctorService : Service<Doctor>, IDoctorService
    {

        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public DoctorService() : base(utk)
        {
        }
    }
}