using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public class CityQuery : IQuery<City>, IConnection
    {
        public DbConnection connection { get; }
        public DbProviderFactory factory { get; }
        private CRUDRows cr = null;
        private ITable tab = null;
        public CityQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;

            cr = new CRUDRows(factory, connection);
        }

        public void Delete(City city)
        {
            cr.DeleteRow("Capitals", "CityId", city.Id);
            cr.DeleteRow("CitiesOfCountries", "CityId", city.Id);
            cr.DeleteRow("Cities", "Id", city.Id);
        }

        public void Insert(City city)
        {
            cr.InsertRow("Cities", city.Name, city.Population);
        }

        public void Update(City city)
        {
            tab.Name = city.Name;
            tab.Num = city.Population;
            cr.UpdateRow("Cities", city.Name, city.Population, tab);
        }
    }
}
