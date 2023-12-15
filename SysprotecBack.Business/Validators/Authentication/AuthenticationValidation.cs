namespace SysprotecBack.Business.Validators.Authentication
{
    using FluentValidation;
    using SysprotecBack.Business.Features.Authentication.Queries;

    public class AuthenticationValidation : AbstractValidator<AuthenticationQuery>
    {
        public AuthenticationValidation()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("El 'Email' es obligatorio.")
                .EmailAddress()
                .WithMessage("el 'Email' no tiene un formato valido.");
        }
    }
}
