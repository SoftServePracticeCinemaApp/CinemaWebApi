using Cinema.Application.DTO.HallDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Helpers.Validations.HallValidation
{
    public class UpdateHallValidator : AbstractValidator<UpdateHallDTO>
    {
        public UpdateHallValidator()
        {
            RuleFor(h => h.Seats)
                .NotEmpty().WithMessage("Місця у залі обов'язкові")
                .Must(seats => seats.All(row => row.Count > 0))
                .WithMessage("Кожен ряд повинен містити хоча б одне місце");
        }
    }
}
