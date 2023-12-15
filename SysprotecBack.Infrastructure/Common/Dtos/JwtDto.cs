namespace SysprotecBack.Infrastructure.Common.Dtos
{
    public class JwtDto
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
