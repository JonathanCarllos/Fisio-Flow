using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FisioFlow_API.Services.Contracts
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _conf);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _conf);

    }
}
