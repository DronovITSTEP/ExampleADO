using System.Data;
using System.Data.Common;


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
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="condition">Условие по какому идентификатору идет отбор</param>
        /// <param name="id">Значение идентификатора</param>
        /// <returns>Возвращает итоговую таблицу</returns>
        public virtual DataTable SelectAllRows(string tableName, string condition, int id)
        {
                adapter.SelectCommand = connection.CreateCommand();
                adapter.SelectCommand.CommandText = GetSelectQuery(tableName, condition, id);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
        }
        /// <summary>
        /// Добавление новой страны в БД
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="name">Название страны</param>
        /// <param name="num">ID части света</param>
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
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="condition">Условие по какому идентификатору идет отбор</param>
        /// <param name="id">Значение идентификатора</param>
        public void DeleteRow(string tableName, string condition, int id)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = GetDeleteQuery(tableName, condition, id);
                using (var builder = factory.CreateCommand())
                {
                    DataTable table = SelectAllRows(tableName, condition, id);
                    if (table.Rows.Count != 0)
                    {
                        table.Rows[0].Delete();

                        adapter.Update(table);
                    }
                }
            }
        }
        /// <summary>
        /// Обновление данных строки из БД
        /// </summary>
        /// <param name="tableName">Имя таблицы</param>
        /// <param name="column1">Первый столбец</param>
        /// <param name="column2">Второй столбец</param>
        /// <param name="name">Значение для первого столбца</param>
        /// <param name="num">Значение для второго столбца</param>
        /// <param name="id">Идентификатор для выбора нужной строки из БД</param>
        public void UpdateRow(string tableName, string column1, string column2, string name, int num, int id)
        {
            using (adapter = factory.CreateDataAdapter())
            {
                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = GetUpdateQuery(tableName, column1, column2, name, num, id);

                using (var builder = factory.CreateCommandBuilder())
                {
                    DataTable table = SelectAllRows(tableName, nameof(id), id);
                    table.Rows[0][column1] = name;
                    table.Rows[0][column2] = num;
                    builder.DataAdapter = adapter;
                    adapter.Update(table);
                }
            }
        }

        private string GetSelectQuery(string tableName, string condition, int id) => $"select * from {tableName} where {condition} = {id}";
        private string GetDeleteQuery(string tableName, string condition, int id) => $"delete from {tableName} where {condition} = {id}";
        private string GetInsertQuery(string table, string name, int num) => $"insert into {table} values ('{name}', {num})";
        private string GetUpdateQuery(string table, string column1, string column2, string name, int num, int id) => $"update {table} set {column1} = '{name}', {column2} = {num} where id = {id}";
    }
}
