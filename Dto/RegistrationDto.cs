using System.ComponentModel.DataAnnotations;

namespace server_app.Dto;

public class RegistrationDto
{
	[Required]
	public string Email { get; set; } = string.Empty;
	[Required]
	public string Password { get; set; } = string.Empty;
}
