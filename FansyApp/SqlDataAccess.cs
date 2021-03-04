using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FansyApp
{
  public class SqlDataAccess : ISqlDataAccess
  {
    public string ConnectionStringName { get; set; } = "Default";

    public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
    {
      var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        var data = await connection.QueryAsync<T>(sql, parameters);
        return data.ToList();
      }
    }
    public async Task SaveData<T>(string sql, T parameters)
    {
      var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
      using (IDbConnection connection = new SqlConnection(connectionString))
      {
        await connection.ExecuteAsync(sql, parameters);
      }
    }
  }
}
