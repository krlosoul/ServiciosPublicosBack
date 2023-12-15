namespace SysprotecBack.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SysprotecBack.Business.Features.Status.Queries;
    using SysprotecBack.Core.Dtos.Status;

    [Authorize]
    [ApiController]
    [Route("api/V1/[controller]")]
    public class StatusController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public StatusController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get status.
        /// </summary>
        /// <returns>The statuses.</returns>
        [HttpGet("GetStatus")]
        public Task<IEnumerable<StatusDto>> GetStatusAsync() => _mediator.Send(new StatusQuery());
    }
}
