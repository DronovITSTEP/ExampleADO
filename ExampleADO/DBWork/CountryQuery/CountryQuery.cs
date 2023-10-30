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

        public CountryQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;
        }

        public void Delete(Country country)
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            // получаем таблицу
            DataTable table = GetDataTable(adapter, country);

            using (var builder = factory.CreateCommandBuilder())
            {
                builder.DataAdapter = adapter;
                adapter.DeleteCommand = builder.GetUpdateCommand();
                adapter.DeleteCommand.CommandText = ConfigurationManager.AppSettings["DeleteCountry"];
                adapter.DeleteCommand.Parameters.AddRange(GetParameters(adapter.DeleteCommand, country));

                adapter.Update(table);
            }
        }

        public void Insert(Country country)
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.InsertCommand = connection.CreateCommand();
            adapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["InsertCountry"];
            adapter.InsertCommand.Parameters.AddRange(GetParameters(adapter.InsertCommand, country));

            adapter.Update(GetDataTable(country));
        }

        public void Update(Country country)
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            // получаем таблицу
            DataTable table = GetDataTable(adapter, country);

            using (var builder = factory.CreateCommandBuilder())
            {
                builder.DataAdapter = adapter;         
                adapter.UpdateCommand = builder.GetUpdateCommand();             
                adapter.UpdateCommand.CommandText = ConfigurationManager.AppSettings["UpdateCountry"];             
                adapter.UpdateCommand.Parameters.AddRange(GetParameters(adapter.UpdateCommand, country));

                adapter.Update(table);
            }
        }

        private DataTable GetDataTable(DbDataAdapter adapter, Country country)
        {
            adapter.SelectCommand = connection.CreateCommand();
            adapter.SelectCommand.CommandText = "select * from Countries where id = " + country.Id + "; select * from PartOfTheWorld where Id = " + country.PartOfTheWorldId;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataTable.AsEnumerable().
                FirstOrDefault()[nameof(country.Name)] = country.Name;
            dataTable.AsEnumerable().
                FirstOrDefault()[nameof(country.PartOfTheWorldId)] = country.PartOfTheWorldId;
            return dataTable;
        }
        private DataTable GetDataTable(Country country)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(nameof(country.Name), typeof(string));
            dataTable.Columns.Add(nameof(country.PartOfTheWorldId), typeof(int));
            DataRow row = dataTable.NewRow();
            row[nameof(country.Name)] = country.Name;
            row[nameof(country.PartOfTheWorldId)] = country.PartOfTheWorldId;
            dataTable.Rows.Add(row);
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
