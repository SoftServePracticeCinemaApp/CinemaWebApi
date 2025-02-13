using Cinema.Application.DTO.HallDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validation
{
    public class AddHallValidator : AbstractValidator<AddHallDTO>
    {
        public AddHallValidator()
        {
            RuleFor(h => h.Seats)
                .NotEmpty().WithMessage("Місця у залі обов'язкові")
                .Must(seats => seats.All(row => row.Count > 0))
                .WithMessage("Кожен ряд повинен містити хоча б одне місце");
        }
    }
}
