using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    public class DeleteRows
    {
        DbProviderFactory factory;
        DbConnection connection;
        public DeleteRows(DbProviderFactory factory, DbConnection connection)
        {
            this.factory = factory;
            this.connection = connection;
        }

        public void DeleteTable(string tableName, string condition, int id)
        {
            using (DbDataAdapter adapter = factory.CreateDataAdapter())
            {
                adapter.SelectCommand = connection.CreateCommand();
                adapter.DeleteCommand = connection.CreateCommand();

                adapter.SelectCommand.CommandText = $"select * from {tableName} where {condition} = {id}";
                adapter.DeleteCommand.CommandText = $"delete from {tableName} where {condition} = {id}";

                using (var builder = factory.CreateCommandBuilder())
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 0) return;
                    table.Rows[0].Delete();

                    adapter.Update(table);
                }
            }
        }
    }
}
