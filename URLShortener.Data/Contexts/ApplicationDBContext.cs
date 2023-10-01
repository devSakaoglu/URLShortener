using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Domain.Identity;
using URLShortener.Shared.Data;

namespace URLShortener.Data.Contexts;

public class ApplicationDbContext: IdentityDbContext<User, ApplicationRole, Guid>, IApplicationDbContext
{
    public DbSet<Link> Links { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=urlshortener.db");
    }
}