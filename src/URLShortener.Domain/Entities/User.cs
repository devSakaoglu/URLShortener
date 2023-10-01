using Microsoft.AspNetCore.Identity;
using URLShortener.Domain.Identity;

namespace URLShortener.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public List<Link> Links { get; set; }
    public ApplicationRole ApplicationRole { get; set; }
}