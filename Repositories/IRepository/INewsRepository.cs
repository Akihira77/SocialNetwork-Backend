using server_app.Models;

namespace server_app.Repositories.IRepository;

public interface INewsRepository : IRepository<News>
{
    void Update(News obj);
}
