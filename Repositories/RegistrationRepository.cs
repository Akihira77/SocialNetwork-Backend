using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Repositories;

public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
{
    private readonly ApplicationDbContext _db;

    public RegistrationRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public void Update(Registration obj)
    {
        _db.Registrations.Update(obj);
    }
}
