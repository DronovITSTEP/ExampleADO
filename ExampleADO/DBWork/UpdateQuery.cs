using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExampleADO.DBWork
{
    public class UpdateQuery
    {
        DbConnection dbConnection;
        DbDataAdapter dbDataAdapter;
        DbCommand dbCommand;
        public UpdateQuery(DbConnection dbConnection, DbProviderFactory dbProviderFactory)
        {
            this.dbConnection = dbConnection;
            dbDataAdapter = dbProviderFactory.CreateDataAdapter();
            dbCommand = dbConnection.CreateCommand();
        }
        public void Insert(object obj)
        {
            dbDataAdapter.InsertCommand = dbCommand;

            if (obj is Country)
            {
                AddParameter(obj as Country);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["InsertCountry"];

                dbDataAdapter.Update(GetDataTable(obj as Country));
            }
            else if (obj is City)
            {
                AddParameter(obj as City);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["InsertCity"];

                dbDataAdapter.Update(GetDataTable(obj as City));
            }

        }
        public void Update(object obj)
        {
            dbDataAdapter.UpdateCommand = dbCommand;
            if (obj is Country)
            {
                AddParameter(obj as Country);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["UpdateCountry"];

                dbDataAdapter.Update(GetDataTable(obj as Country));
            }
            else if (obj is City)
            {
                AddParameter(obj as City);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["UpdateCity"];

                dbDataAdapter.Update(GetDataTable(obj as City));
            }
        }
        public void Delete(object obj)
        {
            dbDataAdapter.UpdateCommand = dbCommand;
            if (obj is Country)
            {
                AddParameter(obj as Country);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["DeleteCountry"];

                dbDataAdapter.Update(GetDataTable(obj as Country));
            }
            else if (obj is City)
            {
                AddParameter(obj as City);
                dbDataAdapter.InsertCommand.CommandText = ConfigurationManager.AppSettings["DeleteCity"];

                dbDataAdapter.Update(GetDataTable(obj as City));
            }
        }
        private void AddParameter(Country country)
        {
            dbCommand.Parameters.Clear();
            DbParameter dbParameter1 = dbCommand.CreateParameter();
            DbParameter dbParameter2 = dbCommand.CreateParameter();
            dbParameter1.ParameterName = "@name";
            dbParameter2.ParameterName = "@part";
            dbParameter1.Value = country.Name;
            dbParameter2.Value = country.PartWorld;
            dbParameter1.DbType = DbType.String;
            dbParameter2.DbType = DbType.Int32;
            dbCommand.Parameters.AddRange(new DbParameter[] { dbParameter1, dbParameter2 });
        }
        private void AddParameter(City city)
        {
            dbCommand.Parameters.Clear();
            DbParameter dbParameter1 = dbCommand.CreateParameter();
            DbParameter dbParameter2 = dbCommand.CreateParameter();
            DbParameter dbParameter3 = dbCommand.CreateParameter();
            DbParameter dbParameter4 = dbCommand.CreateParameter();

            dbParameter1.ParameterName = "@name";
            dbParameter2.ParameterName = "@population";
            dbParameter3.ParameterName = "@countryId";
            dbParameter4.ParameterName = "@capitalId";

            dbParameter1.Value = city.Name;
            dbParameter2.Value = city.Population;
            dbParameter3.Value = city.CountryId;
            //dbParameter4.Value = city.;

            dbParameter1.DbType = DbType.String;
            dbParameter2.DbType = DbType.Int32;
            dbParameter3.DbType = DbType.Int32;

            dbCommand.Parameters.AddRange(new DbParameter[] { dbParameter1, dbParameter2, dbParameter3 });
        }

        private DataTable GetDataTable(Country country)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Part", typeof(int));
            DataRow dataRow = dataTable.NewRow();
            dataRow["Name"] = country.Name;
            dataRow["Part"] = country.PartWorld;
            dataTable.Rows.Add(dataRow);

            return dataTable;
        }
        private DataTable GetDataTable(City city)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name",typeof(string));
            dataTable.Columns.Add("Population", typeof(int));
            dataTable.Columns.Add("CountryId", typeof(int));
            DataRow dataRow = dataTable.NewRow();
            dataRow["Name"] = city.Name;
            dataRow["Population"] = city.Population;
            dataRow["CountryId"] = city.CountryId;

            dataTable.Rows.Add(dataRow);
            return dataTable;
        }
    }
}
