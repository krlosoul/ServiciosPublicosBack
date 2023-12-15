namespace SysprotecBack.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SysprotecBack.Api.Filters;
    using SysprotecBack.Business.Features.Report.Commands;
    using SysprotecBack.Business.Features.Report.Queries;
    using SysprotecBack.Core.Dtos.Report;
    using SysprotecBack.Core.Enums;

    [Authorize]
    [ApiController]
    [Route("api/V1/[controller]")]
    public class ReportController : ControllerBase
    {
        #region Properties
        private readonly IMediator _mediator;
        #endregion

        public ReportController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get reports.
        /// </summary>
        /// <returns>The reports.</returns>
        [HttpGet("GetReports")]        
        public Task<IEnumerable<ReportDto>> GetReportsAsync() => _mediator.Send(new ReportQuery());

        /// <summary>
        /// Get reports by filter.
        /// </summary>
        /// <param name="reportByFilter">The param.</param>
        /// <returns>The reports.</returns>
        [HttpGet("GetReportsByFilter/{IdService}/{IdStatus}/{IdUser}")]
        public Task<IEnumerable<ReportDto>> GetReportByFilterAsync([FromRoute] ReportByFilter reportByFilter) => _mediator.Send(reportByFilter);

        /// <summary>
        /// Register report.
        /// </summary>
        /// <param name="reportCommand">The param.</param>
        /// <returns>Unit.</returns>
        [JwtRolesAuthorization(RolesEnum.Administrator, RolesEnum.Editor)]
        [HttpPost("PostReport")]
        public Task<Unit> PostReportAsync([FromBody] ReportCreateCommand reportCommand) => _mediator.Send(reportCommand);

        /// <summary>
        /// Update report.
        /// </summary>
        /// <param name="reportUpdateCommand">The param.</param>
        /// <returns>Unit.</returns>
        [JwtRolesAuthorization(RolesEnum.Administrator, RolesEnum.Editor)]
        [HttpPut("PutReport")]
        public Task<Unit> PutReportAsync([FromBody] ReportUpdateCommand reportUpdateCommand) => _mediator.Send(reportUpdateCommand);

        /// <summary>
        /// Delete report.
        /// </summary>
        /// <param name="reportDeleteCommand">The param.</param>
        /// <returns>Unit.</returns>
        [JwtRolesAuthorization(RolesEnum.Administrator)]
        [HttpDelete("DeleteReport/{Id}")]
        public Task<Unit> DeleteReportAsync([FromRoute] ReportDeleteCommand reportDeleteCommand) => _mediator.Send(reportDeleteCommand);
    }
}
