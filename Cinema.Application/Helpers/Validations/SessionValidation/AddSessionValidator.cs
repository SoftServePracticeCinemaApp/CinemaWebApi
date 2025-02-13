using Cinema.Application.DTO.SessionDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Helpers.Validations.SessionValidation
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
