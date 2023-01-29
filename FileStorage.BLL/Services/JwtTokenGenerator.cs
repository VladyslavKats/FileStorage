using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileStorage.BLL.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly AuthOptions _options;

        public JwtTokenGenerator(IOptions<AuthOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> GenerateAsync(string userId, IEnumerable<string> roles)
        {
            var securityKey = _options.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier , userId),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityToken = new JwtSecurityToken(
                    _options.Issuer,
                    _options.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(_options.TokenLifetime),
                    signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return await Task.FromResult(token);
        }
    }
}
