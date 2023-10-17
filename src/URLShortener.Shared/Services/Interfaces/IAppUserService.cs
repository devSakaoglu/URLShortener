using URLShortener.Domain.Entities;
using URLShortener.Shared.Models.AppUser;

namespace URLShortener.Shared.Services.Interfaces;

public interface IAppUserService
{
    Task<AppUser> CreateAsync(CreateAppUserModel model);
}