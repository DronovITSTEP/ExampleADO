using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ExampleADO.DBWork
{
    public class CountryQuery : IQuery<Country>, IConnection
    {
        public DbProviderFactory factory { get; }
        public DbConnection connection { get; }

        private CRUDRows cr = null;
        private ITable tab = null;

        public CountryQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;

            cr = new CRUDRows(factory, connection);
        }

        public void Delete(Country country)
        {
            cr.DeleteRow("Capitals","CountryId", country.Id);
            cr.DeleteRow("CitiesOfCountries", "CountryId",country.Id);
            cr.DeleteRow("Countries", "Id", country.Id);
        }

        public void Insert(Country country)
        {
            cr.InsertRow("Countries", country.Name, country.PartOfTheWorldId);
        }

        public void Update(Country country)
        {
            tab.Name = country.Name;
            tab.Num = country.PartOfTheWorldId;
            cr.UpdateRow("Countries", country.Name, country.PartOfTheWorldId, tab);
        }
    }
}
