using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace server_app.Services;

public class PhotoServices : IPhotoServices
{
	private readonly Cloudinary _cloudinary;

    public PhotoServices(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
            );
        _cloudinary = new Cloudinary( acc );
    }
	public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
	{
		var uploadResult = new ImageUploadResult();
		if (file.Length > 0)
		{
			using var stream = file.OpenReadStream();
			var uploadParams = new ImageUploadParams
			{
				File = new FileDescription(file.FileName, stream),
				Transformation = new Transformation().Height(100).Width(200).Crop("fill").Gravity("face"),
				Folder = "socialnetworkAPIImages"
			};
			uploadResult = await _cloudinary.UploadAsync(uploadParams);
		}

		return uploadResult;
	}
	public async Task<DeletionResult> DeletePhotoAsync(string source)
	{
		int end = source.Length;
		if (source.LastIndexOf(".jpg") >= 0) end = source.LastIndexOf(".jpg");
		else if (source.LastIndexOf(".jpeg") >= 0) end = source.LastIndexOf(".jpeg");
		else if (source.LastIndexOf(".png") >= 0) end = source.LastIndexOf(".png");

		//var end = source.LastIndexOf(".jpg") | source.LastIndexOf(".jpeg") | source.LastIndexOf(".png");
		
		var start = source.LastIndexOf("/") + 1;
		var publicId = "socialnetworkAPIImages/" + source[start..end];
		var deleteParams = new DeletionParams(publicId);
		var result = await _cloudinary.DestroyAsync(deleteParams);
		return result;
	}
}
