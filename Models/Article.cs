using System.ComponentModel.DataAnnotations;

namespace server_app.Models;

public class Article
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int IsActive { get; set; } = 1;
    public int IsApproved { get; set; }

    public string Type { get; set; } = string.Empty;
}
