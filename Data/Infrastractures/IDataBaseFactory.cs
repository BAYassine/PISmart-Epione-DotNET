using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Epione.Data.Infrastructures
{
    public interface IDataBaseFactory : IDisposable
    {
        EpioneContext DataContext { get; }
    }
}