using server_app.Models;

namespace server_app.Repositories.IRepository;

public interface IArticleRepository : IRepository<Article>
{
    void Update(Article obj);
}
