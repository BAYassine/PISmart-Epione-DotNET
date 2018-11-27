using Domain.Entities;
using Epione.Data.Infrastructures;
using Service.Pattern;

namespace Service
{
    public class UserService : Service<User>, IUserService
    {

        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public UserService() : base(utk)
        {
        }
    }
}