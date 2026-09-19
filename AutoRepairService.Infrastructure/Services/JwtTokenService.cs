using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces;
using AutoRepairService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AutoRepairService.Infrastructure.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenService(IOptions<JwtSettings> options) // IOptions<JwtSettings>  JwtSettings-ის მონაცემებს იღებს configuration-იდან: appsetting იდან იღებს და კლასად აქცევს რომელიც ჩვენ ტოკენისთვის შევქმენით
        {
            _jwtSettings = options.Value;
        }
        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("email_verified", user.IsEmailVerified.ToString(), ClaimValueTypes.Boolean)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));


            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256); /// ეს token ხელი მოაწერე ამ key-ით და გამოიყენე HMAC-SHA256.

            var toekn = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(toekn); // tokens მივეცით სპეციალური სამ ნაწილიანი ფორმა, header, oayder, signature და ისე დავაბრუნეთ.
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes); 
        }
    }
}
