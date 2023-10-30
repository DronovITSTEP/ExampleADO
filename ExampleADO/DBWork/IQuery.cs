using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public interface IQuery <T>
    {
        DbConnection connection { get; }
        DbProviderFactory factory { get;}
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
