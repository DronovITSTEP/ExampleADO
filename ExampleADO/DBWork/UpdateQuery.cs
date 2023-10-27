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
    public class UpdateQuery
    {
        DbConnection connection = null;
        DbProviderFactory factory = null;

        public UpdateQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;
        }

        public void Update()
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = connection.CreateCommand();
            adapter.SelectCommand.CommandText = "select * from Countries";
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["Id"].ToString() == "2")
                {
                    row["Name"] = "Псков";
                    row["PartOfTheWorldId"] = 3;
                }
            }
            using (var builder = factory.CreateCommandBuilder())
            {
                builder.DataAdapter = adapter;
          
                adapter.UpdateCommand = builder.GetUpdateCommand(true);
              
                adapter.UpdateCommand.CommandText = ConfigurationManager.AppSettings["UpdateCountry"];

                DbParameter param1 = adapter.UpdateCommand.CreateParameter();
                param1.ParameterName = "@name";
                param1.DbType = DbType.String;
                param1.Value = "Псков";

                DbParameter param2 = adapter.UpdateCommand.CreateParameter();
                param2.ParameterName = "@part";
                param2.DbType = DbType.Int32;
                param2.Value = 3;

                adapter.UpdateCommand.Parameters.Add(param1);
                adapter.UpdateCommand.Parameters.Add(param2);

                adapter.Update(dataTable);
            }
        }
    }
}
