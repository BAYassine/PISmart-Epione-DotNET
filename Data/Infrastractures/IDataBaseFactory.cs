using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formation.Data.Infrastructures
{
    public interface IDataBaseFactory :IDisposable
    {
       FormationContext DataContext { get;  }
    }
}
