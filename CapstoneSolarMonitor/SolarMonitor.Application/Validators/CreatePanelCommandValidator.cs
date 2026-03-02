using FluentValidation; 
using SolarMonitor.Application.Commands;

namespace SolarMonitor.Application.Validators; 

public class CreatePanelCommandValidator : AbstractValidator<CreatePanelCommand>
{
    public CreatePanelCommandValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Panel brand is required.")
            .MaximumLength(100)
            .WithMessage("Brand name is too long.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Panel model is required."); 
    }
}