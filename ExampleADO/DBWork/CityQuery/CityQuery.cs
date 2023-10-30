using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork.CityQuery
{
    public class CityQuery : IQuery<City>
    {
        public DbConnection connection { get; }

        public DbProviderFactory factory { get; }
        public CityQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;
        }

        public void Delete(City city)
        {
            throw new NotImplementedException();
        }

        public void Insert(City city)
        {
            throw new NotImplementedException();
        }

        public void Update(City city)
        {
            throw new NotImplementedException();
        }
    }
}
