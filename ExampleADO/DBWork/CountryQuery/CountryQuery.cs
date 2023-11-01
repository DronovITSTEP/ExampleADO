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
    public class CountryQuery : IQuery<Country>
    {
        public DbProviderFactory factory { get; }
        public DbConnection connection { get; }

        DeleteRows dr = null;

        public CountryQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;

            dr = new DeleteRows(factory, connection);
        }

        public void Delete(Country country)
        {
            dr.DeleteTable("Capitals","CountryId", country.Id);
            dr.DeleteTable("CitiesOfCountries", "CountryId",country.Id);
            dr.DeleteTable("Countries", "Id", country.Id);
        }

        public void Insert(Country country)
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.InsertCommand = connection.CreateCommand();
            adapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["InsertCountry"];
            adapter.InsertCommand.Parameters.AddRange(GetParameters(adapter.InsertCommand, country));

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(nameof(country.Name), typeof(string));
            dataTable.Columns.Add(nameof(country.PartOfTheWorldId), typeof(int));
            DataRow row = dataTable.NewRow();
            row[nameof(country.Name)] = country.Name;
            row[nameof(country.PartOfTheWorldId)] = country.PartOfTheWorldId;
            dataTable.Rows.Add(row);

            adapter.Update(dataTable);
        }

        public void Update(Country country)
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            DataTable table = GetDataTable(country);

            using (var builder = factory.CreateCommandBuilder())
            {
                builder.DataAdapter = adapter;
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.UpdateCommand.CommandText = ConfigurationManager.AppSettings["UpdateCountry"];
                adapter.UpdateCommand.Parameters.AddRange(GetParameters(adapter.UpdateCommand, country));

                adapter.Update(table);
            }
        }
        private DataTable GetDataTable(Country country)
        {
            DataTable dataTable = dr.SelectTable("Countries", "id", country.Id);

            dataTable.AsEnumerable().
                FirstOrDefault()[nameof(country.Name)] = country.Name;
            dataTable.AsEnumerable().
                FirstOrDefault()[nameof(country.PartOfTheWorldId)] = country.PartOfTheWorldId;
            return dataTable;
        }
        private DbParameter[] GetParameters(DbCommand com, Country country)
        {
            DbParameter[] parameters = new DbParameter[3];
            parameters[0] = com.CreateParameter();
            parameters[0].ParameterName = "@name";
            parameters[0].DbType = DbType.String;
            parameters[0].Value = country.Name;

            parameters[1] = com.CreateParameter();
            parameters[1].ParameterName = "@part";
            parameters[1].DbType = DbType.Int32;
            parameters[1].Value = country.PartOfTheWorldId;

            parameters[2] = com.CreateParameter();
            parameters[2].ParameterName = "@id";
            parameters[2].DbType = DbType.Int32;
            parameters[2].Value = country.Id;

            return parameters;
        }
    }
}
