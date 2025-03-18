using FluentValidation;

namespace WebApi.Applications.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryValidator: AbstractValidator<GetActorDetailQuery>
    {
        public GetActorDetailQueryValidator()
        {
            RuleFor(x=> x.ActorId).GreaterThan(0).NotEmpty();
        } 
    }
}