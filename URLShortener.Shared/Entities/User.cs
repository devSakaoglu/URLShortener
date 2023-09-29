using Microsoft.AspNetCore.Identity;
namespace URLShortener.Shared.Entities;
public class User : IdentityUser<Guid> 
{
    public List<Link> Links { get; set; }
}

