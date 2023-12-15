namespace SysprotecBack.Business.Commons.Mappers
{
    using AutoMapper;
    using SysprotecBack.Core.Dtos.Claim;
    using SysprotecBack.Core.Dtos.Report;
    using SysprotecBack.Core.Dtos.Service;
    using SysprotecBack.Core.Dtos.Status;
    using SysprotecBack.Core.Dtos.User;
    using SysprotecBack.Core.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, ClaimDto>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => string.Join(",", src.UserRoles.Select(role => role.IdRole))));

            CreateMap<Status, StatusDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Service, ServiceDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, UserDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Report, ReportDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.IdService, opt => opt.MapFrom(src => src.IdService))
               .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Service.Name))
               .ForMember(dest => dest.IdStatus, opt => opt.MapFrom(src => src.IdStatus))
               .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
               .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
               .ForMember(dest => dest.Observation, opt => opt.MapFrom(src => src.Observation))
               .ForMember(dest => dest.CreationOn, opt => opt.MapFrom(src => src.CreationOn));
        }
    }
}
