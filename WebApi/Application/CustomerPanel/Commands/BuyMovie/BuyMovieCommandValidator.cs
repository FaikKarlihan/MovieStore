using FluentValidation;

namespace WebApi.Applications.CustomerPanel.Commands.BuyMovie
{
    public class BuyMovieCommandValidator: AbstractValidator<BuyMovieCommand>
    {
        public BuyMovieCommandValidator()
        {
            RuleFor(x=> x.Email).NotEmpty();
            RuleFor(x=> x.Model.Movie).NotEmpty();
        }
    }
}