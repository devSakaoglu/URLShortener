using URLShortener.Domain.Entities;
using URLShortener.Shared.Models.Link;

namespace URLShortener.Shared.Services.Interfaces;

public interface ILinkService
{
    Task<Link> CreateAsync(CreateLinkModel model);
    Task<Link> UpdateShortAddressAsync(UpdateShortAddressModel model);
    Task<Link> UpdateFullAddressAsync(UpdateFullAddressModel model);
    Task<Link> SetEnabledAsync(SetEnabledModel model);
    Task<ICollection<Link>> GetAllByUserIdAsync(Guid userId);
    Task<Link> GetByIdAsync(GetByIdModel model, bool includeVisits = false);
    Task<Link> GetByShortAddressAsync(GetByShortAddressModel model);
    Task DeleteAsync(DeleteModel model);
}