namespace SysprotecBack.Infrastructure.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using SysprotecBack.Business.Interfaces.Services;
    using SysprotecBack.Core.Dtos.Claim;
    using SysprotecBack.Infrastructure.Common.Constants;
    using SysprotecBack.Infrastructure.Common.Dtos;
    using SysprotecBack.Infrastructure.Extensions;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtService : IJwtService
    {
        #region
        private readonly IConfiguration _configuration;
        private JwtDto? _jwtDto;
        #endregion

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
        }

        public string GenerateToken(ClaimDto claimDto)
        {
            var claimsGenerator = ClaimsGenerator.Generate(claimDto);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtDto?.SecretKey ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsGenerator),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtDto?.Issuer,
                Audience = _jwtDto?.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ClaimDto GetDataByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validatedToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var IdUser = validatedToken.Claims.First(c => c.Type == "IdUser").Value;
            var UserName = validatedToken.Claims.First(c => c.Type == "UserName").Value;
            var Roles = validatedToken.Claims.First(c => c.Type == "Roles").Value;

            return new ClaimDto { IdUser = Convert.ToInt32(IdUser), UserName = UserName, Roles = Roles };
        }

        #region PrivateMethod
        private void GetConfiguration()
        {
            JwtDto instance = _jwtDto = new JwtDto();
            _configuration.Bind(JwtConstant.JwtConfig, instance);
            instance.SecretKey = _jwtDto.SecretKey;
            instance.Issuer = _jwtDto.Issuer;
            instance.Audience = _jwtDto.Audience;
        }
        #endregion
    }
}
