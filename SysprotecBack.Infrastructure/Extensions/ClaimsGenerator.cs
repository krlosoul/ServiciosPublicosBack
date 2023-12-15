namespace SysprotecBack.Infrastructure.Extensions
{
    using SysprotecBack.Core.Dtos.Claim;
    using System.Collections.Generic;
    using System.Security.Claims;

    public static class ClaimsGenerator
    {
        public static IEnumerable<Claim> Generate(ClaimDto claimDto)
        {
            var claims = new List<Claim>();

            if (claimDto?.IdUser != null)
                claims.Add(new Claim("IdUser", claimDto.IdUser.ToString()));

            if (claimDto?.UserName != null)
                claims.Add(new Claim("UserName", claimDto.UserName));

            if (claimDto?.Roles != null)
                claims.Add(new Claim("Roles", claimDto.Roles));

            return claims;
        }
    }
}
