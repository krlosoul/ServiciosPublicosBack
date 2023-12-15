namespace SysprotecBack.Business.Features.Status.Queries
{
    using AutoMapper;
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Status;
    using SysprotecBack.Core.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class StatusQuery : IRequest<IEnumerable<StatusDto>> { }

    public class AuthenticationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<StatusQuery, IEnumerable<StatusDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<StatusDto>> Handle(StatusQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Status.GetAllAsync() ?? throw new NotFoundException("No hay estados en el sistema.");
            var result = _mapper.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(data);

            return result;
        }
    }
}
