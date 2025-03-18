using FluentValidation;

namespace WepApi.Applications.CustomerPanel.Queries.GetMyMovies
{
    public class GetMyMoviesQueryValidator: AbstractValidator<GetMyMoviesQuery>
    {
        public GetMyMoviesQueryValidator()
        {
            RuleFor(x=> x.CustomerEmail).NotEmpty();
        }
    }
}