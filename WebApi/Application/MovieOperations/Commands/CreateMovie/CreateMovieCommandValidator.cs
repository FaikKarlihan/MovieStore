using System.Linq;
using FluentValidation;

namespace WebApi.Applications.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator: AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(x => x.Model.Actors)
                .NotEmpty().WithMessage("At least one actor must be provided.")
                .Must(actors => actors.All(name => name.Split(' ').Length > 1))
                .WithMessage("Each actor must have a full name (Name Surname).");
            RuleFor(x=> x.Model.Director)
                .NotEmpty().WithMessage("At least one director must be provided.")
                .Must(director => director.Split(' ').Length > 1)
                .WithMessage("Each director must have a full name (Name Surname).");

            RuleFor(x=> x.Model.Name).NotEmpty(); //*****
            RuleFor(x=> x.Model.Genre).NotEmpty();
            RuleFor(x=> x.Model.Price).NotEmpty().GreaterThan(0);
            RuleFor(x=> x.Model.PublishDate).NotEmpty();
        }
    }
}