using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Repositories;

public class NewsRepository : Repository<News>, INewsRepository
{
    private readonly ApplicationDbContext _db;

    public NewsRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(News obj)
    {
        _db.News.Update(obj);
    }
}
