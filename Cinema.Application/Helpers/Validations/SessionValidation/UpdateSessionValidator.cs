using Cinema.Application.DTO.SessionDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Helpers.Validations.SessionValidation
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
