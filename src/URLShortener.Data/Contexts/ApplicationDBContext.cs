using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Shared.Data;

namespace URLShortener.Data.Contexts;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Visit> Visits { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUsers");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email)
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();
        });

        builder.Entity<Link>(entity =>
        {
            entity.HasOne(e => e.AppUser)
                .WithMany(u => u.Links)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ShortAddress)
                .IsUnique();
        });

        builder.Entity<Visit>(entity =>
        {
            entity.HasOne(e => e.Link)
                .WithMany(l => l.Visits)
                .HasForeignKey(e => e.LinkId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    public new void SaveChanges()
    {
        base.SaveChanges();
    }
}