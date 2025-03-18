using FluentValidation;

namespace WebApi.Applications.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator: AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(x=> x.MovieId).GreaterThan(0).NotEmpty();
        } 
    }
}