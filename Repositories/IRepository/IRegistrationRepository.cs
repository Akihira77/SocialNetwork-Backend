using server_app.Models;

namespace server_app.Repositories.IRepository;

public interface IRegistrationRepository : IRepository<Registration>
{
    void Update(Registration obj);
}
