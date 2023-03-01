using System.ComponentModel.DataAnnotations;

namespace server_app.Models;

public class Staff
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Name { get; set; } = string.Empty;
	[Required]
	public string Email { get; set; } = string.Empty;
	[Required]
	public string Password { get; set; } = string.Empty;
	public int IsActive { get; set; } = 1;
}
