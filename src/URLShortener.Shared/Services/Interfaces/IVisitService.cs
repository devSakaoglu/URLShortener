using URLShortener.Domain.Entities;
using URLShortener.Shared.Models.Visit;

namespace URLShortener.Shared.Services.Interfaces;

public interface IVisitService
{
        public Task<List<Visit>> GelAllByLinkIdAsync(GelAllByLinkIdModel model);
        public Task<ICollection<Visit>> GetAllByUserIdAsync(GetAllByUserIdModel model);
        
        public Task<Visit> CreateAsync(CreateVisitModel model);
        public Task UpdateGeoDataAsync(UpdateGeoDataModel model);
        
}

