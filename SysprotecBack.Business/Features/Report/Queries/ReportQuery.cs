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
    using System.Threading.Tasks;

    public class ReportQuery : IRequest<IEnumerable<ReportDto>> { }

    public class ReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ReportQuery, IEnumerable<ReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ReportDto>> Handle(ReportQuery request, CancellationToken cancellationToken)
        {
            var include = new Expression<Func<Report, object>>[]
            {
                p => p.Service,
                p => p.Status,
                p => p.User
            };
            var data = await _unitOfWork.Report.GetAllAsync(includeProperties: include) ?? throw new NotFoundException("No hay reportes en el sistema.");
            var result = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportDto>>(data);

            return result;
        }
    }
}
