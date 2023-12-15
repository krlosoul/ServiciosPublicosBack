namespace SysprotecBack.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SysprotecBack.Business.Features.Service.Queries;
    using SysprotecBack.Core.Dtos.Service;

    [Authorize]
    [ApiController]
    [Route("api/V1/[controller]")]
    public class ServiceController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public ServiceController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get services.
        /// </summary>
        /// <returns>The services.</returns>
        [HttpGet("GetService")]
        public Task<IEnumerable<ServiceDto>> GetServiceAsync() => _mediator.Send(new ServiceQuery());
    }
}
