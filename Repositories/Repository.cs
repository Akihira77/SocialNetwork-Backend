using Microsoft.EntityFrameworkCore;
using server_app.Data;
using server_app.Repositories.IRepository;
using System.Linq.Expressions;

namespace server_app.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, bool noTrack = false)
    {
        IQueryable<T> query = (noTrack ? dbSet.AsNoTracking() : dbSet);

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveAll(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}
