using Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Epione.Data.Infrastructures;

namespace Service
{
    public class NotificationService : NotificationService<Notificationapp>, INotificationService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();

        static IUnitOfWork utk = new UnitOfWork(Factory);
        public NotificationService() : base(utk)
        {

        }


    }

}