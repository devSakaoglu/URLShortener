using Microsoft.EntityFrameworkCore;
using URLShortener.Domain.Entities;
using URLShortener.Shared.Data;
using URLShortener.Shared.Models.Visit;
using URLShortener.Shared.Services.Interfaces;

namespace URLShortener.Services.Implementations;

public class VisitService : IVisitService
{
    private readonly IApplicationDbContext _context;

    public VisitService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Visit>> GelAllByLinkIdAsync(GelAllByLinkIdModel model)
    {
        var result = await _context.Links
            .Where(l => l.Id == model.Id && l.UserId == model.UserId)
            .Include(l => l.Visits)
            .SelectMany(s => s.Visits)
            .AsNoTracking()
            .ToListAsync();

        return result;
    }

    public async Task<ICollection<Visit>> GetAllByUserIdAsync(GetAllByUserIdModel model)
    {
        var visits = await _context.Links.Where(l => l.UserId == model.UserId)
            .Include(l => l.Visits)
            .SelectMany(s => s.Visits)
            .AsNoTracking()
            .ToListAsync();

        return visits;
    }


    public async Task<Visit> CreateAsync(CreateVisitModel model)
    {
        var visit = new Visit
        {
            LinkId = model.LinkId,
            IpAddress = model.IpAddress,
            Country = model.Country,
            City = model.City
        };
        await _context.Visits.AddAsync(visit);
        await _context.SaveChangesAsync();
        return visit;
    }

    public async Task UpdateGeoDataAsync(UpdateGeoDataModel model)
    {
        var visit = await _context.Visits.SingleAsync(v => v.Id == model.VisitId);
        visit.Country = model.Country;
        visit.City = model.City;

        await _context.SaveChangesAsync();
    }
}