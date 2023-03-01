namespace server_app.Dto;

public class ImageUploadDto
{
    public string FileName { get; set; }
    public IFormFile FormFile { get; set; }
}
