using FluentValidation;

namespace WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres
{
    public class GetMyFavoriteGenresQueryValidator: AbstractValidator<GetMyFavoriteGenresQuery>
    {
        public GetMyFavoriteGenresQueryValidator()
        {
            RuleFor(x=> x.CustomerEmail).NotEmpty();
        }
    }
}