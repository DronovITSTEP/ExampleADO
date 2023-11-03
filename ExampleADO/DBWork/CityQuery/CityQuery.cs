using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public class CityQuery : AbstractQuery
    {     
        public CityQuery(DbConnection connection, DbProviderFactory factory): base(connection, factory){}

        public override void Delete<T>(T city)
        {
            cr.DeleteRow("Capitals", "CityId", (city as City).Id);
            cr.DeleteRow("CitiesOfCountries", "CityId", (city as City).Id);
            cr.DeleteRow("Cities", "Id", (city as City).Id);
        }
        public override void Insert<T>(T city)
        {
            cr.InsertRow("Cities", (city as City).Name, (city as City).Population);
        }
        public override void Update<T>(T city)
        {
           /* tab.Name = (city as City).Name;
            tab.Num = (city as City).Population;
            cr.UpdateRow("Cities", (city as City).Name, (city as City).Population, tab);*/
        }
    }
}
