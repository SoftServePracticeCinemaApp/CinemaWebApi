using Cinema.Application.DTO.MovieDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validations.MovieValidation
{
    public class AddMovieValidator : AbstractValidator<AddMovieDTO>
    {
        public AddMovieValidator()
        {
            RuleFor(m => m.SearchId)
                .GreaterThan(0).WithMessage("ID пошуку має бути більше 0");
        }
    }
}
