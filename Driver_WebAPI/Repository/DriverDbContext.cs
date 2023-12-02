using Dapper;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Driver_WebAPI.Repository;

/// <summary>
/// Our Driver Context
/// </summary>
public class DriverDbContext :IDisposable
{
    private readonly SqliteConnection _dbConnection;
    private readonly string _scriptPath;
    public DriverDbContext(string connectionString, string scriptPath)
    {
        _scriptPath = scriptPath;
        Batteries.Init();
        _dbConnection = new SqliteConnection(connectionString);
        _dbConnection.Open();
        // Create the 'Driver' table if it does not exist
        CreateDriverTable();
    }

    /// <summary>
    /// Used to retrieve data from a database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>return sequence of data of type T</returns>
    public IEnumerable<T> Query<T>(string sql, object parameters = null!)
    {
        return _dbConnection.Query<T>(sql, parameters);
    }

    /// <summary>
    ///  Used to execute SQL commands that do not return data
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    public int Execute(string sql, object parameters = null!)
    {
        return _dbConnection.Execute(sql, parameters);
    }

    public void Dispose()
    {
        _dbConnection.Close();
        _dbConnection.Dispose();
    }
    /// <summary>
    /// Create Table if it not exists
    /// </summary>
    private void CreateDriverTable()
    {
        var path = Path.Combine(_scriptPath, "CreateTable.sql");
        var script = File.ReadAllText(path);

        using (var command = new SqliteCommand(script, _dbConnection))
        {
            command.ExecuteNonQuery();
        }
    }
}
