﻿namespace SysprotecBack.Business
{
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using SysprotecBack.Business.Commons.Behaviours;
    using SysprotecBack.Business.Commons.Mappers;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
