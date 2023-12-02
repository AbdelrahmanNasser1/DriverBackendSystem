namespace Driver_WebAPI.Interfaces;

/// <summary>
/// Generic repository pattern for exclude ORM working from services 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDriverRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(string id);
    void Add(T entity);
    void Update(T entity);
    void Delete(string id);
}