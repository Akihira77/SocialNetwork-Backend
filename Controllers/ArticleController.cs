using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_app.Dto;
using server_app.Models;
using server_app.Repositories.IRepository;
using server_app.Services;

namespace server_app.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPhotoServices _photoServices;

	public ArticleController(IUnitOfWork unitOfWork, IPhotoServices photoServices)
    {
		_unitOfWork = unitOfWork;
		_photoServices = photoServices;
	}

	[HttpGet("get-all-articles")]
	public async Task<Response> GetAllArticle()
	{
		var response = new Response();

		var obj = await _unitOfWork.Article.GetAll((a => a.IsActive == 1));

		if (obj == null)
		{
			response.StatusCode = 100;
			response.StatusMessage = "No Article data found";

			return response;
		}

		response.StatusCode = 200;
		response.StatusMessage = "Article data";
		response.ArticleList = obj.ToList();
		return response;
	}

	[HttpPost("get-articles-by-type")]
	public async Task<Response> GetAllArticle(ArticleDto articleDto)
	{
		var response = new Response();

		// if type is Page then get all
		var obj = await _unitOfWork.Article.GetAll((a => a.IsActive == 1));

		if (obj == null)
		{
			response.StatusCode =100;
			response.StatusMessage = "No Article data found";

			return response;
		}

		if (string.Equals(articleDto.Type.ToUpper(), "USER"))
		{
			obj = obj.Where(a => string.Equals(a.Email, articleDto.Email)).ToList();
		}

		response.StatusCode = 200;
		response.StatusMessage = "Article data";
		response.ArticleList = obj.ToList();
		return response;
	}

	[HttpPost("addArticle")]
	public async Task<Response> Create(Article article)
	{
		var response = new Response();

		await _unitOfWork.Article.Add(article);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Article created";
		return response;
	}

	[HttpPost("updateArticle")]
	public async Task<Response> Update(Article article)
	{
		var response = new Response();
		var obj = await _unitOfWork.Article.GetFirstOrDefault((a => a.Id ==  article.Id), true);

		obj.Title = article.Title;
		obj.Content = article.Content;
		_unitOfWork.Article.Update(obj);
		await _unitOfWork.Save();

		response.StatusCode = 200;
		response.StatusMessage = "Article updated";
		return response;
	}

	[HttpPost("uploadFile")]
	public async Task<Response> Create([FromForm] ImageUploadDto file)
	{
		var response = new Response();
		var imageUrl = await _photoServices.AddPhotoAsync(file.FormFile);

		response.StatusCode = 200;
		response.StatusMessage = "Image uploaded";
		response.ImageUrl = imageUrl.Url.ToString();
		return response;
	}

	[HttpPatch("articleApproval")]
	public async Task<Response> ArticleApproval(int id)
	{
		var response = new Response();

		var obj = await _unitOfWork.Article
				.GetFirstOrDefault((a => a.Id == id && a.IsActive == 1), true);

		if (obj != null)
		{
			obj.IsApproved = 1 - obj.IsApproved;
			_unitOfWork.Article.Update(obj);
			await _unitOfWork.Save();

			response.StatusCode = 200;
			response.StatusMessage = $"user article {obj.Email} approved";
			return response;
		}

		response.StatusCode = 100;
		response.StatusMessage = "User is not found";
		return response;
	}

	[HttpPatch("DeleteArticle")] 
	public async Task<Response> DeleteArticle(int id)
	{
		var response = new Response();
		var obj = await _unitOfWork.Article.GetFirstOrDefault((a => a.Id == id));
		if (obj != null)
		{
			var result = await _photoServices.DeletePhotoAsync(obj.Image);

			if (result.Result.ToUpper() == "OK")
			{
				_unitOfWork.Article.Remove(obj);
				await _unitOfWork.Save();

				response.StatusCode = 200;
				response.StatusMessage = "Article deleted successfully";
				return response;
			}
		}

		response.StatusCode = 400;
		response.StatusMessage = "Article deleted failed";
		return response;
	}

	[HttpPatch("DeletePhoto")]
	public async Task<Response> DeletePhoto(string Url)
	{
		var response = new Response();
		try
		{
			var res = await _photoServices.DeletePhotoAsync(Url);
			if (res.Result == "not found")
			{
				response.StatusCode = 100;
				response.StatusMessage = "Image not found";
				return response;
			}
		} catch (Exception)
		{
			response.StatusCode = 400;
			response.StatusMessage = "Image deleted failed";
			return response;
		}

		response.StatusCode = 200;
		response.StatusMessage = "Image deleted successfully";
		return response;
	}
}
