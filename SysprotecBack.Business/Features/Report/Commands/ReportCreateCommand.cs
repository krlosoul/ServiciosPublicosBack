namespace SysprotecBack.Business.Features.Report.Commands
{
    using MediatR;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Report;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReportCreateCommand : ReportCreateDto, IRequest{}

    public class ReportCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ReportCreateCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(ReportCreateCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Report.ExecuteStoreProcedureNonQueryAsync("[Sp_Post_Report]", request);
            return Unit.Value;
        }
    }
}
