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
    public class CountryQuery : AbstractQuery
    {
        public CountryQuery(DbConnection connection, DbProviderFactory factory) : base(connection, factory){ }

        public override void Delete<T>(T country)
        {
            cr.DeleteRow("Capitals","CountryId", (country as Country).Id);
            cr.DeleteRow("CitiesOfCountries", "CountryId", (country as Country).Id);
            cr.DeleteRow("Countries", "Id", (country as Country).Id);
        }

        public override void Insert<T>(T country)
        {
            cr.InsertRow("Countries", (country as Country).Name, (country as Country).PartOfTheWorldId);
        }

        public override void Update<T>(T country)
        {
            Country c = country as Country;
            cr.UpdateRow("Countries", c.Name, c.PartOfTheWorldId,
                nameof(c.Name),
                nameof(c.PartOfTheWorldId));
        }
    }
}
