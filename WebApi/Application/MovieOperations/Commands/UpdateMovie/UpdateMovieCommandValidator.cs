using FluentValidation;

namespace WebApi.Applications.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator: AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x=> x.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}