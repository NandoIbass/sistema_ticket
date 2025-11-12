using FluentValidation;
using HelpDesk.Application.DTOs;

namespace HelpDesk.Application.Validators
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Priority)
                .NotEmpty()
                .Must(p => new[] { "Baja", "Media", "Alta" }.Contains(p))
                .WithMessage("Prioridad inválida (Baja, Media, Alta).");
        }
    }
}