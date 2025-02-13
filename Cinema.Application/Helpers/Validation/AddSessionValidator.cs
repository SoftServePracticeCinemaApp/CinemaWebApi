using Cinema.Application.DTO.SessionDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validation
{
    public class AddSessionValidator : AbstractValidator<AddSessionDTO>
    {
        public AddSessionValidator()
        {
            RuleFor(s => s.MovieId)
                .GreaterThan(0).WithMessage("ID фільму має бути більше 0");

            RuleFor(s => s.Date)
                .GreaterThan(DateTime.UtcNow).WithMessage("Дата сеансу повинна бути в майбутньому");

            RuleFor(s => s.TicketPrice)
                .GreaterThan(0).WithMessage("Ціна квитка повинна бути більше 0");
        }
    }
}
