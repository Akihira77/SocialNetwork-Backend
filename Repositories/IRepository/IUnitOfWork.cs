using server_app.Services;

namespace server_app.Repositories.IRepository;

public interface IUnitOfWork 
{
    IArticleRepository Article { get; }
    IEventRepository Events { get; }
    INewsRepository News { get; }
    IRegistrationRepository Registration { get; }
    IStaffRepository Staff { get; }
    Task Save();
}
