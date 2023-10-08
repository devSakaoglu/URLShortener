namespace URLShortener.API.Middlewares;

public class JwtTokenFromCookieMiddleware
{
    private readonly RequestDelegate _next;

    public JwtTokenFromCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            if (context.Request.Cookies.TryGetValue("JWT_TOKEN_COOKIE", out var jwtToken))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            }
        }

        await _next(context);
    }
}