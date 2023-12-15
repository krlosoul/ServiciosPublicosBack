namespace SysprotecBack.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SysprotecBack.Business.Features.User.Queries;
    using SysprotecBack.Core.Dtos.User;

    [Authorize]
    [ApiController]
    [Route("api/V1/[controller]")]
    public class UserController
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public UserController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get users.
        /// </summary>
        /// <returns>The users.</returns>
        [HttpGet("GetUsers")]
        public Task<IEnumerable<UserDto>> GetUsersAsync() => _mediator.Send(new UserQuery());
    }
}
