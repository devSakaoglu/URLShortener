using Microsoft.AspNetCore.Identity;
using URLShortener.Shared.Identity;

namespace URLShortener.Shared.Entities;
public class User : IdentityUser<Guid> 
{
    public List<Link> Links { get; set; }
    public ApplicationRole ApplicationRole { get; set; }
}