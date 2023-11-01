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
        DeleteRows dr;
        public CityQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;
            dr = new DeleteRows(factory, connection);
        }

        public void Delete(City city)
        {
            dr.DeleteTable("Capitals", "CityId", city.Id);
            dr.DeleteTable("CitiesOfCountries", "CityId", city.Id);
            dr.DeleteTable("City", "Id", city.Id);
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
