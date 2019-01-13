using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epione.Data.Infrastructure;

namespace Epione.Data.Infrastructures
{
    public interface IUnitOfWork
    {
        void Commit(); //gérer les transaction
        void Dispose();//libérer l'espace mémoire du ctx
        IRepositoryBase<T> GetRepositoryBase<T>() where T : class; //CRUG générique
    }
}