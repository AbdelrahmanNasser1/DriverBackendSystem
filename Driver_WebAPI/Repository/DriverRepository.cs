using Driver_WebAPI.Interfaces;

namespace Driver_WebAPI.Repository;

/// <summary>
/// contains our sqlLite CRUD operations and implement the generic repoistory BY Using Dapper
/// </summary>
public class DriverRepository<T> : IDriverRepository<T> where T : class
{
    private readonly DriverDbContext _dbContext;

    public DriverRepository(DriverDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    /// <summary>
    /// Used to retrieve all data in table
    /// </summary>
    /// <returns>IEnumerable of type T</returns>
    public IEnumerable<T> GetAll()
    {
        return _dbContext.Query<T>($"SELECT * FROM {typeof(T).Name}");
    }

    /// <summary>
    /// Used to retrieve only one record that match the Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>object of type T</returns>
    public T GetById(string id)
    {
        return _dbContext.Query<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id }).FirstOrDefault()!;
    }

    /// <summary>
    /// Add a record of Type T to the table
    /// </summary>
    /// <param name="entity"></param>
    public void Add(T entity)
    {
        _dbContext.Execute($"INSERT INTO {typeof(T).Name} VALUES (@Id, @FirstName, @LastName, @Email, @PhoneNumber)", entity);
    }

    /// <summary>
    /// Update the record based on its Id
    /// </summary>
    /// <param name="entity"></param>
    public void Update(T entity)
    {
        _dbContext.Execute($"UPDATE {typeof(T).Name} SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber  WHERE Id = @Id", entity);
    }

    /// <summary>
    /// Delete the record Based on its Id
    /// </summary>
    /// <param name="id"></param>
    public void Delete(string id)
    {
        _dbContext.Execute($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
    }

}
