namespace SysprotecBack.Business.Interfaces.Services
{
    using SysprotecBack.Core.Dtos.Claim;

    public interface IJwtService
    {
        /// <summary>
        /// Generate token by user.
        /// </summary>
        /// <param name="claimDto">Data result of autentication.</param>
        /// <returns>Token.</returns>
        public string GenerateToken(ClaimDto claimDto);

        /// <summary>
        /// Get user information by token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Data result of autentication.</returns>
        public ClaimDto GetDataByToken(string token);
    }

}
