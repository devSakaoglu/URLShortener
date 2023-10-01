using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;

namespace URLShortener.Shared.Data;

public interface IApplicationDbContext
{
    DbSet<Link> Links { get; }
    DbSet<Visit> Visits { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}