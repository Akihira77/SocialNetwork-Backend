using server_app.Data;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Repositories;

public class StaffRepository : Repository<Staff>, IStaffRepository
{
	private readonly ApplicationDbContext _db;
	public StaffRepository(ApplicationDbContext db) : base(db)
	{
		_db = db;
	}

	public void update(Staff obj)
	{
		_db.Staffs.Update(obj);
	}
}
