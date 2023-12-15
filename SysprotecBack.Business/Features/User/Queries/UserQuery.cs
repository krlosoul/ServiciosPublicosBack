namespace SysprotecBack.Business.Features.User.Queries
{
    using AutoMapper;
    using MediatR;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Dtos.User;
    using SysprotecBack.Core.Entities;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserQuery : IRequest<IEnumerable<UserDto>> { }

    public class UserQueryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UserQuery, IEnumerable<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserDto>> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.User.GetAllAsync() ?? throw new NotFoundException("No hay usuarios en el sistema.");
            var result = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(data);

            return result;
        }
    }
}
