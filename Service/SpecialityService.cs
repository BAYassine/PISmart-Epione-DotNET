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
   public class SpecialityService: Service<Speciality>,ISpecialityService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public SpecialityService() : base(utk)
        {
        }
    }
}
