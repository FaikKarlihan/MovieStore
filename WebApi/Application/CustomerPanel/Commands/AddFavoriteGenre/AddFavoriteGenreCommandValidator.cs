using FluentValidation;

namespace WebApi.Applications.CustomerPanel.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandValidator: AbstractValidator<AddFavoriteGenreCommand>
    {
        public AddFavoriteGenreCommandValidator()
        {
            RuleFor(x=> x.Email).NotEmpty();
            RuleFor(x=> x.Model.Genre).NotEmpty();
        }
    }
}