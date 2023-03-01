using server_app.Models;

namespace server_app.Repositories.IRepository;

public interface IEventRepository : IRepository<Events>
{
    void Update(Events obj);
}
