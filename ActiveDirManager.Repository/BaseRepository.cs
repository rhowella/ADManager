using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ActiveDirManager.Repository
{
  public class BaseRepository : IDisposable
  {
    protected IDbConnection _db;

    public BaseRepository()
    {
      string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
      //"Data Source=localhost;Initial Catalog=ActiveDirManager;Integrated Security=True";
      _db = new SqlConnection(connectionString);
    }
     
    public void Dispose()
    {
      _db.Close();
    }
  }
}