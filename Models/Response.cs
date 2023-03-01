namespace server_app.Models;

public class Response
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;

    public IEnumerable<Registration> RegistrationList { get; set; }
    public IEnumerable<Staff> StaffList { get; set; }
    public Registration Registration { get; set; }
    public IEnumerable<Article> ArticleList { get; set; }
    public IEnumerable<News> NewsList { get; set; }
    public IEnumerable<Events> EventsList { get; set; }
}
