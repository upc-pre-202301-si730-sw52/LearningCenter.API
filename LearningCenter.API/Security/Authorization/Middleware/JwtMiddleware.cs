using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Authorization.Settings;
using LearningCenter.API.Security.Domain.Services;

namespace LearningCenter.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, AppSettings appSettings)
    {
        _next = next;
        _appSettings = appSettings;
    }

    public async Task Invoke(
        HttpContext context,
        IUserService userService,
        IJwtHandler handler)
    {
        
        // Get Token
        
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ")
            .Last();
        
        // Extract UserId
        var userId = handler.ValidateToken(token);

        if (userId != null)
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        
        // Call next in chain
        
        await _next(context);

    }
}