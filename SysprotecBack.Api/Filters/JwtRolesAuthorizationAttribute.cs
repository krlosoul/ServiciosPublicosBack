namespace SysprotecBack.Api.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Core.Enums;

    public class JwtRolesAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        #region Params
        private readonly RolesEnum[] _requiredRoles;
        #endregion

        public JwtRolesAuthorizationAttribute(params RolesEnum[] requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var rolesClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Roles");
            if (rolesClaim == null)
            {
                context.Result = new ForbidResult();
                return;
            }
            var roles = rolesClaim.Value.Split(',');
            foreach (var role in roles)
            {
                if (Enum.TryParse(role, out RolesEnum parsedRole) &&
                    Enum.IsDefined(typeof(RolesEnum), parsedRole) &&
                    _requiredRoles.Contains(parsedRole))
                {
                    return; 
                }
            }
            context.Result = new ForbidResult();
        }
    }
}
