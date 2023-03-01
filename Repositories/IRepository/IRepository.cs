using System.Linq.Expressions;

namespace server_app.Repositories.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);
    Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, bool noTrack = false);
    Task Add(T entity);
    void Remove(T entity);
    void RemoveAll(IEnumerable<T> entity);
}
