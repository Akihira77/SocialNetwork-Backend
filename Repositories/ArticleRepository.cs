using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    private readonly ApplicationDbContext _db;

    public ArticleRepository(ApplicationDbContext db) : base(db)
	{
        _db = db;
    }

    public void Update(Article obj)
    {
        _db.Articles.Update(obj);
    }
}
