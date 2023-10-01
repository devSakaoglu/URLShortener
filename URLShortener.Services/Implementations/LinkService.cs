using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Shared.Data;
using URLShortener.Shared.Models.Link;
using URLShortener.Shared.Services.Interfaces;

namespace URLShortener.Services.Implementations;

public class LinkService : ILinkService
{
    private readonly IApplicationDbContext _context;

    public LinkService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Link> CreateAsync(CreateLinkModel model)
    {
        var link = new Link()
        {
            ShortAddress = model.ShortAddress,
            FullAddress = model.FullAddress,
            UserId = model.UserId
        };
        await _context.Links.AddAsync(link);
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task<Link> UpdateShortAddressAsync(UpdateShortAddressModel model)
    {
        var link = await GetByIdAsync(new GetByIdModel { UserId = model.UserId, Id = model.Id });
        link.ShortAddress = model.ShortAddress;
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task<Link> UpdateFullAddressAsync(UpdateFullAddressModel model)
    {
        var link = await GetByIdAsync(new GetByIdModel { UserId = model.UserId, Id = model.Id });
        link.FullAddress = model.FullAddress;
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task<Link> SetEnabledAsync(SetEnabledModel model)
    {
        var link = await GetByIdAsync(new GetByIdModel { UserId = model.UserId, Id = model.Id });
        link.IsEnabled = model.IsEnabled;
        await _context.SaveChangesAsync();
        return link;
    }

    public async Task<ICollection<Link>> GetAllByUserIdAsync(Guid userId)
    {
        var linkList = await _context.Links.Where(l => l.UserId == userId).ToListAsync();
        return linkList;
    }

    public async Task<Link> GetByIdAsync(GetByIdModel model, bool includeVisits = false)
    {
        var query = _context.Links.AsQueryable();
        if (includeVisits)
        {
            query = query.Include(l => l.Visits);
        }

        return await query
            .Where(l => l.UserId == model.UserId)
            .SingleAsync(l => l.Id == model.Id);
    }

    public async Task<Link> GetByShortAddressAsync(GetByShortAddressModel model)
    {
        var link = await _context.Links
            .Where(l => l.UserId == model.UserId)
            .SingleAsync(l => l.ShortAddress == model.ShortAddress);
        return link;
    }

    public async Task DeleteAsync(DeleteModel model)
    {
        var link = await GetByIdAsync(new GetByIdModel { UserId = model.UserId, Id = model.Id });
        link.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}