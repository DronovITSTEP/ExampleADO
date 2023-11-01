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
        DbDataAdapter adapter;
        public DeleteRows(DbProviderFactory factory, DbConnection connection)
        {
            this.factory = factory;
            this.connection = connection;
            adapter = factory.CreateDataAdapter();
        }
        public DataTable SelectTable(string tableName, string condition, int id)
        {
            adapter.SelectCommand = connection.CreateCommand();
            adapter.SelectCommand.CommandText = $"select * from {tableName} where {condition} = {id}";
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public void DeleteTable(string tableName, string condition, int id)
        {
            adapter.DeleteCommand = connection.CreateCommand();
            adapter.DeleteCommand.CommandText = $"delete from {tableName} where {condition} = {id}";
            using (var builder = factory.CreateCommandBuilder())
            {
                DataTable table = SelectTable(tableName, condition, id);
                if (table.Rows.Count == 0) return;
                table.Rows[0].Delete();

                adapter.Update(table);
            }
        }
    }
}
