namespace SysprotecBack.Business.Validators.Report
{
    using FluentValidation;
    using SysprotecBack.Business.Features.Report.Commands;

    public class ReportUpdateValidation : AbstractValidator<ReportUpdateCommand>
    {
        public ReportUpdateValidation()
        {
            RuleFor(r => r.Id)
                .NotNull()
                .WithMessage("El 'Id' es obligatorio.");

            RuleFor(r => r.IdService)
                .NotNull()
                .WithMessage("El 'IdService' es obligatorio.");

            RuleFor(r => r.IdStatus)
                .NotNull()
                .WithMessage("El 'IdStatus' es obligatorio.");

            RuleFor(r => r.Observation)
                .NotEmpty()
                .WithMessage("El 'Observation' es obligatorio.");
        }
    }
}
