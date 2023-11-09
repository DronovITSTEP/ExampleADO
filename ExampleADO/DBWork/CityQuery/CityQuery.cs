using ExampleADO.Models;
using System.Data.Common;


namespace ExampleADO.DBWork
{
    public class CityQuery : AbstractQuery
    {
        private City city;
        public CityQuery(DbConnection connection, DbProviderFactory factory): base(connection, factory){}

        public override void Delete<T>(T c)
        {
            city = c as City;
            cr.DeleteRow("Capitals", "CityId", city.Id);
            cr.DeleteRow("CitiesOfCountries", "CityId", city.Id);
            cr.DeleteRow("Cities", "Id", city.Id);
        }
        public override void Insert<T>(T c)
        {
            city = c as City;
            cr.InsertRow("Cities", city.Name, city.Population);
        }
        public override void Update<T>(T c)
        {
            city = c as City;
            cr.UpdateRow("Cities", nameof(city.Name), nameof(city.Population),
                city.Name,
                city.Population, city.Id);
        }
    }
}
