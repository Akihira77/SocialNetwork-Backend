using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_app.Models;
using server_app.Repositories.IRepository;

namespace server_app.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;

	public NewsController(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	[HttpGet("get-all-news")]
	public async Task<Response> GetAllNews()
	{
		var response = new Response();
		var obj = await _unitOfWork.News.GetAll((n => n.IsActive == 1));

		response.StatusCode = (obj != null ? 200 : 100);
		response.StatusMessage = (obj != null ? "News data" : "No News data found");
		response.NewsList = obj?.ToList();
		return response;

	}

	[HttpPost("mynews")]
	public async Task<Response> GetMyNews(string email)
	{
		var response = new Response();
		var obj = await _unitOfWork.News.GetAll((n => n.IsActive == 1 && n.Email == email));

		response.StatusCode = (obj != null ? 200 : 100);
		response.StatusMessage = (obj != null ? "News data" : "Your news data data found");
		response.NewsList = obj?.ToList();
		return response;

	}

	[HttpPost("addNews")]
	public async Task<Response> Create(News news)
	{
		var response = new Response();

		await _unitOfWork.News.Add(news);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "News created";
		return response;
	}
}
