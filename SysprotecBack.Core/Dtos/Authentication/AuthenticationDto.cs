namespace SysprotecBack.Core.Dtos.Authentication
{
    public class AuthenticationRequestDto
    {
        public string? Email { get; set; }
    }

    public class AuthenticationResponseDto
    {
        public string? Token { get; set;}
    }
}
