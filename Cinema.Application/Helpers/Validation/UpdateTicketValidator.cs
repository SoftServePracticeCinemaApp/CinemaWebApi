using Cinema.Application.DTO.TicketDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validation
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketDTO>
    {
        public UpdateTicketValidator()
        {
            RuleFor(t => t.SessionId)
                .GreaterThan(0).WithMessage("ID сеансу має бути більше 0");

            RuleFor(t => t.MovieId)
                .GreaterThan(0).WithMessage("ID фільму має бути більше 0");

            RuleFor(t => t.Row)
                .GreaterThan(0).WithMessage("Ряд має бути більше 0");
        }
    }
}
