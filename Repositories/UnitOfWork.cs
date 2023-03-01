using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;
using server_app.Services;

namespace server_app.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Events = new EventRepository(_db);
        Article = new ArticleRepository(_db);
        News = new NewsRepository(_db);
        Registration = new RegistrationRepository(_db);
        Staff = new StaffRepository(_db);
    }
    public IRegistrationRepository Registration { get; private set; }
    public IArticleRepository Article { get; private set; }
    public INewsRepository News { get; private set; }
    public IEventRepository Events { get; private set; }
    public IStaffRepository Staff { get; private set; }
    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}
