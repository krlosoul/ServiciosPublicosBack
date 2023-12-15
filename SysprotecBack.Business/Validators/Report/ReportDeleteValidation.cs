namespace SysprotecBack.Business.Validators.Report
{
    using FluentValidation;
    using SysprotecBack.Business.Features.Report.Commands;

    public class ReportDeleteValidation : AbstractValidator <ReportDeleteCommand>
    {
        public ReportDeleteValidation()
        {
            RuleFor(r => r.Id)
                .NotNull()
                .WithMessage("El 'Id' es obligatorio.");
        }
    }
}
