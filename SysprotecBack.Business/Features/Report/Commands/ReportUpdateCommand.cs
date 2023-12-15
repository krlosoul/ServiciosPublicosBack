namespace SysprotecBack.Business.Features.Report.Commands
{
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Report;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReportUpdateCommand : ReportUpdateDto, IRequest { }

    public class ReportUpdateCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ReportUpdateCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(ReportUpdateCommand request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Report.FirstOrDefaultAsync(x => x.Id.Equals(request.Id)) ?? throw new NotFoundException("No existe reporte en el sistema.");
            data.IdService = request.IdService;
            data.IdStatus = request.IdStatus;
            data.IdUser = request.IdUser;
            data.Observation = request.Observation;            
            await _unitOfWork.Report.UpdateAsync(data);

            return Unit.Value;
        }
    }
}
