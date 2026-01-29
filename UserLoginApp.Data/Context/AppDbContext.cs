using Microsoft.EntityFrameworkCore;
using UserLoginApp.Data.Entities;

namespace UserLoginApp.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
}
