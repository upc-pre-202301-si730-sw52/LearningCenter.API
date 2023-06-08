using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}