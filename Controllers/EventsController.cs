using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;

	public EventsController(IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
	}

	[HttpGet("get-all-events")]
	public async Task<Response> GetAllEvents()
	{
		var response = new Response();

		var obj = await _unitOfWork.Events.GetAll((a => a.IsActive == 1));

		if (obj == null)
		{
			response.StatusCode = 100;
			response.StatusMessage = "No Events data found";

			return response;
		}

		response.StatusCode = 200;
		response.StatusMessage = "Events data";
		response.EventsList = obj.ToList();
		return response;
	}

	[HttpPost("addEvent")]
	public async Task<Response> Create(Events events)
	{
		var response = new Response();

		await _unitOfWork.Events.Add(events);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Events created";
		return response;
	}
}
