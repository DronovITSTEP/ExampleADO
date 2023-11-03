using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public interface IConnection 
    {
        DbConnection connection { get; }
        DbProviderFactory factory { get;}       
    }
    public interface IQuery<T>
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
