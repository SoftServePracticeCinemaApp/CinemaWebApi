using Cinema.Application.DTO.MovieDTOs;
using FluentValidation;

namespace Cinema.Application.Helpers.Validation
{
    public class UpdateMovieValidator : AbstractValidator<UpdateMovieDTO>
    {
        public UpdateMovieValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage("Назва фільму обов'язкова")
                .MaximumLength(255).WithMessage("Назва не може перевищувати 255 символів");

            RuleFor(m => m.CinemaRating)
                .InclusiveBetween(0, 10).WithMessage("Рейтинг має бути від 0 до 10");
        }
    }
}
