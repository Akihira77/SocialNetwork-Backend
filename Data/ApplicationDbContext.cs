using Microsoft.EntityFrameworkCore;
using server_app.Models;

namespace server_app.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Events> Events { get; set; }
    public DbSet<Staff> Staffs { get; set;}
}
