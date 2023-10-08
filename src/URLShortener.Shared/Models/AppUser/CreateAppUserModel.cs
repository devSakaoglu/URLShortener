using System.ComponentModel.DataAnnotations;

namespace URLShortener.Shared.Models.AppUser;

public class CreateAppUserModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    // TODO we need to create attirbutes for default identity password requirements
    // and error messages for validation
    public string Password { get; set; }
}