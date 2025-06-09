using Application.Companies.GetByIsin;
using FluentValidation;
using MediatR;

namespace Application.Companies.Update;

public class CompanyUpdateValidator : AbstractValidator<CompanyUpdateRequest>
{
    public CompanyUpdateValidator(IMediator mediator)
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .MaximumLength(128).WithMessage("Name cannot be longer than 128 characters.");

        RuleFor(c => c.Ticker)
            .NotEmpty().WithMessage("Ticker must not be empty")
            .MaximumLength(64).WithMessage("Ticker cannot be longer than 64 characters.");

        RuleFor(c => c.Exchange)
            .NotEmpty().WithMessage("Exchange must not be empty")
            .MaximumLength(128).WithMessage("Exchange cannot be longer than 128 characters.");

        RuleFor(c => c.Isin)
            .NotEmpty().WithMessage("ISIN must not be empty")
            .Length(12).WithMessage("ISIN must be exactly 12 characters.")
            .Must(isin => isin.Length >= 2 && char.IsLetter(isin[0]) && char.IsLetter(isin[1]))
                .WithMessage("The first two characters of ISIN must be letters.")
            .MustAsync(async (request, isin, cancellationToken) =>
            {
                var existing = await mediator.Send(new CompanyGetByIsinRequest { Isin = isin }, cancellationToken);

                return existing == null || existing.Id == request.Id;
            })
            .WithMessage("ISIN already exists for another company.");

        RuleFor(c => c.Website)
            .MaximumLength(1024).WithMessage("Website cannot be longer than 1024 characters.");
    }
}
