namespace SysprotecBack.Business.Validators.Report
{
    using FluentValidation;
    using SysprotecBack.Business.Features.Report.Commands;

    public class ReportCreateValidation : AbstractValidator<ReportCreateCommand>
    {
        public ReportCreateValidation()
        {
            RuleFor(r => r.IdService)
                .NotNull()
                .WithMessage("El 'IdService' es obligatorio.");

            RuleFor(r => r.IdStatus)
                .NotNull()
                .WithMessage("El 'IdStatus' es obligatorio.");

            RuleFor(r => r.IdUser)
                .NotNull()
                .WithMessage("El 'IdUser' es obligatorio.");

            RuleFor(r => r.Observation)
                .NotEmpty()
                .WithMessage("El 'Observation' es obligatorio.");
        }
    }
}
