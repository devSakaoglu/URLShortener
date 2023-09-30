using Microsoft.AspNetCore.Identity;
using URLShortener.Domain.Enums;

namespace URLShortener.Domain.Identity;
public class ApplicationRole : IdentityRole<Guid>
{
    public UserType UserType { get; set; } = UserType.User;
}