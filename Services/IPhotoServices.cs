using CloudinaryDotNet.Actions;

namespace server_app.Services;

public interface IPhotoServices
{
	Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
	Task<DeletionResult> DeletePhotoAsync(string publicId);
}
