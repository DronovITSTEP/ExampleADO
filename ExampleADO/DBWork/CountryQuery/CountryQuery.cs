using ExampleADO.Models;
using System.Data.Common;


namespace ExampleADO.DBWork
{
    public class CountryQuery : AbstractQuery
    {
        private Country country;
        public CountryQuery(DbConnection connection, DbProviderFactory factory) : base(connection, factory){ }

        public override void Delete<T>(T c)
        {
            country = c as Country;

            cr.DeleteRow("Capitals","CountryId", country.Id);
            cr.DeleteRow("CitiesOfCountries", "CountryId", country.Id);
            cr.DeleteRow("Countries", "Id", country.Id);
        }

        public override void Insert<T>(T c)
        {
            country = c as Country;
            cr.InsertRow("Countries", country.Name, country.PartOfTheWorldId);
        }

        public override void Update<T>(T c)
        {
            country = c as Country;
            cr.UpdateRow("Countries", nameof(country.Name), nameof(country.PartOfTheWorldId),
                country.Name,
                country.PartOfTheWorldId, country.Id);
        }
    }
}
