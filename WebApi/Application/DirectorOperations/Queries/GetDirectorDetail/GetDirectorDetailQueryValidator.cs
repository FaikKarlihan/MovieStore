using FluentValidation;

namespace WebApi.Applications.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryValidator: AbstractValidator<GetDirectorDetailQuery>
    {
        public GetDirectorDetailQueryValidator()
        {
            RuleFor(x=> x.DirectorId).GreaterThan(0).NotEmpty();
        } 
    }
}