using Microsoft.AspNetCore.Identity;
using URLShortener.Domain.Enums;

namespace URLShortener.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public List<Link> Links { get; set; }
    public UserType UserType { get; set; }
}