using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExampleADO.DBWork
{
    public class CRUDRows
    {
        DbProviderFactory factory;
        DbConnection connection;
        DbDataAdapter adapter;
        public CRUDRows(DbProviderFactory factory, DbConnection connection)
        {
            this.factory = factory;
            this.connection = connection;
        }
        /// <summary>
        /// Отображение полной информации о переданной таблице из БД;
        /// </summary>
        /// <param name="tableName">имя таблицы</param>
        /// <param name="condition">условие по какому идентификатору идет отбор</param>
        /// <param name="id">значение идентификатора</param>
        /// <returns>возвращает итоговую таблицу</returns>
        public virtual DataTable SelectAllRows(string tableName, string condition, int id)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = GetSelectQuery(tableName, condition, id);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
        public void InsertRow(string tableName, string name, int num)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.InsertCommand = connection.CreateCommand();
                adapter.InsertCommand.CommandText = GetInsertQuery(tableName, name, num);

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add(nameof(name), typeof(string));
                dataTable.Columns.Add(nameof(num), typeof(int));
                DataRow row = dataTable.NewRow();
                row[nameof(name)] = name;
                row[nameof(num)] = num;
                dataTable.Rows.Add(row);

                adapter.Update(dataTable);
            }
        }
        /// <summary>
        /// Удаление строки из таблицы БД по идентификатору;
        /// </summary>
        /// <param name="tableName">имя таблицы</param>
        /// <param name="condition">условие по какому идентификатору идет отбор</param>
        /// <param name="id">значение идентификатора</param>
        public void DeleteRow(string tableName, string condition, int id)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = GetDeleteQuery(tableName, condition, id);
                using (var builder = factory.CreateCommandBuilder())
                {
                    DataTable table = SelectAllRows(tableName, condition, id);
                    if (table.Rows.Count == 0) return;
                    table.Rows[0].Delete();

                    adapter.Update(table);
                }
            }
        }
        public void UpdateRow(string tableName, string name, int num, string column1, string column2)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = GetUpdateQuery(tableName, name, num, column1, column2);

                using (var builder = factory.CreateCommandBuilder())
                {
                    DataTable table = SelectAllRows(tableName, name, num);
                    table.Rows[0][nameof(name)] = name;
                    table.Rows[0][nameof(num)] = num;
                    builder.DataAdapter = adapter;
                    adapter.Update(table);
                }
            }
        }

        private string GetSelectQuery(string tableName, string condition, int id) => $"select * from {tableName} where {condition} = {id}";
        private string GetDeleteQuery(string tableName, string condition, int id) => $"delete from {tableName} where {condition} = {id}";
        private string GetInsertQuery(string table, string name, int num) => $"insert into {table} values ('{name}', {num})";
        private string GetUpdateQuery(string table, string name, int num, string column1, string column2) => $"update {table} set {column1} = '{name}', {column2} = {num}";
    }
}
