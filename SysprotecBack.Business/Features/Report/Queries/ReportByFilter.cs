namespace SysprotecBack.Business.Features.Report.Queries
{
    using AutoMapper;
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Report;
    using SysprotecBack.Core.Entities;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReportByFilter : ReportFilterDto, IRequest<IEnumerable<ReportDto>> { }

    public class ReportByFilterHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReportByFilter, IEnumerable<ReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ReportDto>> Handle(ReportByFilter request, CancellationToken cancellationToken)
        {
            var include = new Expression<Func<Report, object>>[]
            {
                p => p.Service,
                p => p.Status,
                p => p.User
            };

            var data = await _unitOfWork.Report.GetAllAsync(
                where: x => 
                x.IdStatus == request.IdStatus || x.IdService == request.IdService || x.IdUser == request.IdUser ,
                includeProperties: include) ?? throw new NotFoundException("No hay reportes que coincidan con la busqueda.");
            var result = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>(data);

            return result;
        }
    }
}
