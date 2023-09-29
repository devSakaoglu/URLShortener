using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Shared.Entities;
using URLShortener.Shared.Identity;

namespace URLShortener.Data.Contexts;

public class ApplicationDBContext: IdentityDbContext<User, ApplicationRole, Guid>
{
    public DbSet<Link> Links { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=urlshortener.db");
    }
}