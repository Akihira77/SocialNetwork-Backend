using server_app.Models;

namespace server_app.Repositories.IRepository;

public interface IStaffRepository : IRepository<Staff>
{
	void update(Staff obj);
}
