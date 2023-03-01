using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Repositories;

public class EventRepository : Repository<Events>, IEventRepository
{
    private readonly ApplicationDbContext _db;
    public EventRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Events obj)
    {
        _db.Events.Update(obj);
    }
}
