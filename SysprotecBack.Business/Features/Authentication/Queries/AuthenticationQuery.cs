namespace SysprotecBack.Business.Features.Authentication.Queries
{
    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Business.Interfaces.Services;
    using SysprotecBack.Core.Dtos.Authentication;
    using SysprotecBack.Core.Dtos.Claim;
    using SysprotecBack.Core.Entities;
    using System.Threading;
    using System.Threading.Tasks;

    public class AuthenticationQuery : AuthenticationRequestDto, IRequest<AuthenticationResponseDto> { }

    public class AuthenticationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IMemoryCache memoryCache) : IRequestHandler<AuthenticationQuery, AuthenticationResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IMemoryCache _memoryCache = memoryCache;

        public async Task<AuthenticationResponseDto> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
        {
            AuthenticationResponseDto authenticationResponseDto;
            if (_memoryCache.TryGetValue(request.Email, out string cachedToken))
            {
                authenticationResponseDto = new() { Token = cachedToken };
                return authenticationResponseDto;
            }
            var data = await _unitOfWork.User.FirstOrDefaultAsync(x => x.Email.Equals(request.Email), y => y.UserRoles) ?? throw new NotFoundException("Usuario no registrado en sistema.");
            var claim = _mapper.Map<User, ClaimDto>(data);
            string token = _jwtService.GenerateToken(claim);
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600)
            };
            _memoryCache.Set(request.Email, token, cacheEntryOptions);
            authenticationResponseDto = new() { Token = token };
            return authenticationResponseDto;
        }
    }
}
