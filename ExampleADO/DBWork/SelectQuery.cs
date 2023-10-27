using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.DBWork
{
    // присоединенный режим
    public class SelectQuery
    {
        private DbConnection dbConnection = null;
        private DbCommand dbCommand = null;
        private DbDataReader dbDataReader = null;
        public SelectQuery(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            dbConnection.Open();
            dbCommand = dbConnection.CreateCommand();
        }

        //1.Отображение полной информации о странах;
        async public Task<DataView> SelectAll()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectAll"];
            return await ReadBase(dbCommand);
        }
        //2.Отображение частичной информации о странах.Количество информации определяется пользователем;
        
        //3.Отображение информации о конкретной стране;
        async public Task<DataView> SelectCountry(string country)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectCountry"];
            AddParameter(country);
            return await ReadBase(dbCommand);
        }
        //4.Отображение информации о городах конкретной страны
        async public Task<DataView> SelectCityInCountry(string country)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectCityInCountry"];
            AddParameter(country);
            return await ReadBase(dbCommand);
        }
        //5.Отображение всех стран, чьё имя начинается с буквы, указанной пользователем;
        async public Task<DataView> SelectCountryStartChar(string c)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectCountryStartChar"];
            AddParameter(c);
            return await ReadBase(dbCommand);
        }
        //6.Отображение всех столиц, чьё имя начинается с буквы, указанной пользователем;
        async public Task<DataView> SelectCapitalStartChar(string c)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectCapitalStartChar"];
            AddParameter(c);
            return await ReadBase(dbCommand);
        }
        //7.Показать топ-3 столиц с наименьшим количеством жителей;
        async public Task<DataView> SelectTop3CapitalMinPopulation()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectTop3CapitalMinPopulation"];
            return await ReadBase(dbCommand);
        }
        //8.Показать топ-3 стран с наименьшим количеством жителей;
        async public Task<DataView> SelectTop3CountryMinPopulation()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectTop3CountryMinPopulation"];
            return await ReadBase(dbCommand);
        }
        //9.Показать среднее количество жителей в столицах по каждой части света.
        async public Task<DataView> SelectAvgPopulationInCapital()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectAvgPopulationInCapital"];
            return await ReadBase(dbCommand);
        }
        //10.Показать топ-3 стран по каждой части света с наименьшим количеством жителей;
        async public Task<DataView> SelectTop3PartWorldMinPopulation()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectTop3PartWorldMinPopulation"];
            return await ReadBase(dbCommand);
        }
        //11.Показать топ-3 стран по каждой части света с наибольшим количеством жителей;
        async public Task<DataView> SelectTop3PartWorldMaxPopulation()
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectTop3PartWorldMaxPopulation"];
            return await ReadBase(dbCommand);
        }
        //12.Показать среднее количество жителей в конкретной стране;
        async public Task<DataView> SelectAvgPopulationInCountry(string country)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectAvgPopulationInCountry"];
            AddParameter(country);
            return await ReadBase(dbCommand);
        }
        //13.Показать город с наименьшим количеством жителей в конкретной стране.
        async public Task<DataView> SelectCityWhithMinPopulationInCountry(string country)
        {
            dbCommand.CommandText = ConfigurationManager.AppSettings["SelectCityWhithMinPopulationInCountry"];
            AddParameter(country);
            return await ReadBase(dbCommand);
        }

        async private Task<DataView> ReadBase(DbCommand db)
        {
            DataTable dataTable = new DataTable();
            using (dbDataReader = await db.ExecuteReaderAsync())
            {
                int line = 0;
                do
                {
                    while (await dbDataReader.ReadAsync())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < dbDataReader.FieldCount; i++)
                                dataTable.Columns.Add(dbDataReader.GetName(i));
                            line++;
                        }
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < dbDataReader.FieldCount; i++)
                            row[i] = await dbDataReader.GetFieldValueAsync<object>(i);

                        dataTable.Rows.Add(row);
                    }
                } while (await dbDataReader.NextResultAsync());
                return dataTable.DefaultView;
            }
        }
        private void AddParameter(string str)
        {
            dbCommand.Parameters.Clear();
            DbParameter dbParameter = dbCommand.CreateParameter();
            dbParameter.Value = str;
            dbParameter.ParameterName = "@param";
            dbParameter.DbType = DbType.String;
            dbCommand.Parameters.Add(dbParameter);
        }
    }
}
