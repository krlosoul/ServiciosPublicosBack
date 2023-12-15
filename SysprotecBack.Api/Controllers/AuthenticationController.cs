namespace SysprotecBack.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using SysprotecBack.Business.Features.Authentication.Queries;
    using SysprotecBack.Core.Dtos.Authentication;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public AuthenticationController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Authentication.
        /// </summary>
        /// <param name="authenticationQuery">The param.</param>
        /// <returns>The token.</returns>
        [HttpGet("Authentication/{Email}")]
        public Task<AuthenticationResponseDto> AuthenticationAsync([FromRoute] AuthenticationQuery authenticationQuery) => _mediator.Send(authenticationQuery);
    }
}
