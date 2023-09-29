using Microsoft.AspNetCore.Identity;
using URLShortener.Shared.Enums;

namespace URLShortener.Shared.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public UserType UserType { get; set; }
}