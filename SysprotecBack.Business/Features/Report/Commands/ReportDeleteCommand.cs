namespace SysprotecBack.Business.Features.Report.Commands
{
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Report;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReportDeleteCommand : ReportDeleteDto, IRequest { }

    public class ReportDeleteCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ReportDeleteCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(ReportDeleteCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Report.FirstOrDefaultAsync(x => x.Id.Equals(request.Id)) ?? throw new NotFoundException("No existe reporte en el sistema.");
            await _unitOfWork.Report.DeleteAsync(data);

            return Unit.Value;
        }
    }
}
