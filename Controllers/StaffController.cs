using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Controllers;
[Route("api/[controller]")]
[ApiController]
//[EnableCors("CORSPolicy")]
public class StaffController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;

	public StaffController(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	[HttpPost("register")]
	public async Task<Response> Register(Staff staff)
	{
		var response = new Response();

		var isEmailAlreadyTaken = await _unitOfWork.Staff.GetFirstOrDefault((r => r.Email == staff.Email));
		if (isEmailAlreadyTaken != null)
		{
			response.StatusCode = 400;
			response.StatusMessage = "Email is already in use";
			return response;
		}

		await _unitOfWork.Staff.Add(staff);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Staff registration successful";
		return response;
	}

	[HttpDelete("remove")]
	public async Task<Response> DeleteStaff(Staff staff)
	{
		var response = new Response();

		var obj = await _unitOfWork.Staff.GetFirstOrDefault(s => s.Id == staff.Id);

		if (obj == null)
		{
			response.StatusCode = 100;
			response.StatusMessage = "Staff is not available";
			return response;
		}

		_unitOfWork.Staff.Remove(staff);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Staff deleted successful";
		return response;
	}
}
