using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructures
{
    public class DataBaseFactory : Disposable,IDataBaseFactory
    {

        private EpioneContext datacontext;

        public EpioneContext DataContext
        {
            get { return datacontext; }
            
        }

        public DataBaseFactory()
        {
            datacontext = new EpioneContext();
        }

        public override void DisposeCore()
        {
            if (datacontext != null)
                datacontext.Dispose();
        }
    }
}
