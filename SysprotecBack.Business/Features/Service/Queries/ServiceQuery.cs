namespace SysprotecBack.Business.Features.Service.Queries
{
    using AutoMapper;
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.Service;
    using SysprotecBack.Core.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ServiceQuery : IRequest<IEnumerable<ServiceDto>> { }

    public class ServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ServiceQuery, IEnumerable<ServiceDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ServiceDto>> Handle(ServiceQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Service.GetAllAsync() ?? throw new NotFoundException("No hay servicios en el sistema.");
            var result = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceDto>>(data);

            return result;
        }
    }
}
