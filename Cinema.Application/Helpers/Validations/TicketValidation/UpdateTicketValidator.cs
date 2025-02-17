using Cinema.Application.DTO.TicketDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Helpers.Validations.TicketValidation
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
