using Cinema.Application.DTO.SessionDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validation
{
    public class UpdateSessionValidator : AbstractValidator<UpdateSessionDTO>
    {
        public UpdateSessionValidator()
        {
            RuleFor(s => s.Date)
                .GreaterThan(DateTime.UtcNow).WithMessage("Дата сеансу повинна бути в майбутньому");

            RuleFor(s => s.TicketPrice)
                .GreaterThan(0).WithMessage("Ціна квитка повинна бути більше 0");
        }
    }
}
