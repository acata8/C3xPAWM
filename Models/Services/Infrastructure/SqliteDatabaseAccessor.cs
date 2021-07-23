using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace C3xPAWM.Models.Services.Infrastructure
{

    public struct Injector{
        public string query;
        public List<SqliteParameter> list;
    }
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        //Usato per query semplici che non richiedono tabelle nested
      

        public async Task<DataSet> BasicQueryAsync(string q, string citta)
        {
            using(var conn = new SqliteConnection("Data Source=data/C3PAWM.db")){
               
                await conn.OpenAsync();

                using(var cmd = new SqliteCommand(q, conn)){
                    var parameter = new SqliteParameter("citta", citta);
                    cmd.Parameters.Add(parameter);
                    var reader = await cmd.ExecuteReaderAsync();
                    DataSet dataSet = new DataSet();
                    DataTable dataTable = new DataTable();
                    dataSet.Tables.Add(dataTable);
                    dataTable.Load(reader);
                    return dataSet;
                }
            }
        }

        public async Task<DataSet> BasicQueryAsync(string q)
        {
            using(var conn = new SqliteConnection("Data Source=data/C3PAWM.db")){
               
                await conn.OpenAsync();

                using(var cmd = new SqliteCommand(q, conn)){
                    var reader = await cmd.ExecuteReaderAsync();
                    DataSet dataSet = new DataSet();
                    DataTable dataTable = new DataTable();
                    dataSet.Tables.Add(dataTable);
                    dataTable.Load(reader);
                    return dataSet;
                }
            }
        }


    }
}