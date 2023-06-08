using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Authorization.Settings;
using LearningCenter.API.Security.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearningCenter.API.Security.Authorization.Handlers.Implementations;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;

    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        // Generate Token for a valid period of 7 days
        
        Console.WriteLine($"Secret: {_appSettings.Secret}");
        var secret = _appSettings.Secret;
        var key = Encoding.ASCII.GetBytes(secret);
        Console.WriteLine($"Secret key: {key.Length}");
        Console.WriteLine($"User Id: {user.Id.ToString()}");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            }),
            
            Expires = DateTime.UtcNow.AddDays(7),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        Console.WriteLine($"Token Expiration: {tokenDescriptor.Expires.ToString()}");

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public int? ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}