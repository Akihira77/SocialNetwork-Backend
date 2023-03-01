using System.ComponentModel.DataAnnotations;

namespace server_app.Dto;

public class ArticleDto
{
    [Required]
    public string Type { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
