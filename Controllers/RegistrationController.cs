using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_app.Dto;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Controllers;

[Route("api/[controller]")]
[ApiController]
//[AutoValidateAntiforgeryToken]
public class RegistrationController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;

	public RegistrationController(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	[HttpGet("get-all-account")]
	public async Task<Response> GetAllAccount()
	{
		var response = new Response();

		var obj = await _unitOfWork.Registration.GetAll();

		response.StatusCode = 200;
		response.StatusMessage = "Registration successful";
		response.RegistrationList = obj.ToList();
		return response;
	}

	[HttpGet("registrationList/{userType}")]
	public async Task<Response> RegistrationList(string userType)
	{
		var response = new Response();

		if (string.Equals(userType.ToUpper(), "STAFF"))
		{
			var obj = await _unitOfWork.Staff
		.GetAll(s => s.IsActive == 1);

			response.StatusCode = 200;
			response.StatusMessage = "Get all staff list";
			response.StaffList = obj.ToList();
		} else
		{
			var obj = await _unitOfWork.Registration
					.GetAll(r => r.UserType.ToUpper() == userType.ToUpper() && r.IsActive == 1);

			response.StatusCode = 200;
			response.StatusMessage = "Get all registration list";
			response.RegistrationList = obj.ToList();
		}
		return response;
	}

	[HttpPost("register")]
	public async Task<Response> Register(Registration registration)
	{
		var response = new Response();

		var isEmailAlreadyTaken = await _unitOfWork.Registration
					.GetFirstOrDefault((r => r.Email == registration.Email));
		if (isEmailAlreadyTaken != null)
		{
			response.StatusCode = 400;
			response.StatusMessage = "Email is already in use";
			return response;
		}

		await _unitOfWork.Registration.Add(registration);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Registration successful";
		return response;
	}

	[HttpPost("staff-register")]
	public async Task<Response> StaffRegister(Staff staff)
	{
		var response = new Response();

		var isEmailAlreadyTaken = await _unitOfWork.Staff
					.GetFirstOrDefault((r => r.Email == staff.Email));
		if (isEmailAlreadyTaken != null)
		{
			response.StatusCode = 400;
			response.StatusMessage = "Email is already in use";
			return response;
		}

		await _unitOfWork.Staff.Add(staff);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Registration successful";
		return response;
	}

	[HttpPost("login")]
	public async Task<Response> Login(RegistrationDto registrationDto)
	{
		var response = new Response();

		var user = await _unitOfWork.Registration
				.GetFirstOrDefault(r => r.Email == registrationDto.Email);

		if (user != null
				&& string.Equals(user.Password, registrationDto.Password))
		{
			if (user.IsApproved == 1 || user.UserType.ToUpper() == "ADMIN") 
			{
				response.StatusCode = 200;
				response.StatusMessage = $"user {user.Name} login successfully";
			} else
			{
				response.StatusCode = 100;
				response.StatusMessage = $"user {user.Name}, your Registration is still pending for approval";
			}
			response.Registration = user;
			return response;
		}

		response.StatusCode = 100;
		response.StatusMessage = "Login failed";
		return response;
	}

	[HttpPatch("userApproval")]
	public async Task<Response> UserApproval(int id)
	{
		var response = new Response();

		var user = await _unitOfWork.Registration
				.GetFirstOrDefault((r => r.Id == id && r.IsActive == 1), true);

		if (user != null)
		{
			user.IsApproved = 1;
			_unitOfWork.Registration.Update(user);
			await _unitOfWork.Save();

			response.StatusCode = 200;
			response.StatusMessage = $"user {user.Email} approved";
			return response;
		}

		response.StatusCode = 100;
		response.StatusMessage = "User is not found";
		return response;
	}
}
