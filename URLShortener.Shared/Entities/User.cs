using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using URLShortener.Shared.Enums;
using Microsoft.AspNetCore.Identity;
namespace URLShortener.Shared.Entities;
public class User : IdentityUser<Guid> 
{
    public List<Link> Links { get; set; }
}

