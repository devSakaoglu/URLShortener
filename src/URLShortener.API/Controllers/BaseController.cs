using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace URLShortener.API.Controllers;

public class BaseController : ControllerBase
{
    [NonAction]
    protected Guid GetUserIdFromJwtClaim()
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