using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Domain.Identity;
using URLShortener.Shared.Data;

namespace URLShortener.Data.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public new DbSet<User> Users { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Visit> Visits { get; set; }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    public new void SaveChanges()
    {
        base.SaveChanges();
    }
}