using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MobilePractice.Dtos;
using MobilePractice.Models;

namespace MobilePractice.Services;

 public class JwtService {
    private readonly JwtSetttings _jwtSettings;

    public JwtService(IConfiguration config) {
        _jwtSettings = config.GetSection("Jwt").Get<JwtSetttings>();
    }

    public string GenerateToken(PractitionerDto practitioner) {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, practitioner.Id.ToString()),
            new Claim(ClaimTypes.Name, practitioner.Email),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiryInMinutes),
            SigningCredentials = credentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

 }