using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace URLShortener.API.Controllers;

public abstract class BaseController : ControllerBase
{
    public Guid GetUserIdFromJwtClaim()
    {
        var userClaim = Request.HttpContext.User.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
        var result = Guid.TryParse(userClaim, out var userId);

        if (!result)
        {
            throw new Exception("User not found");
        }

        return userId;
    }
}