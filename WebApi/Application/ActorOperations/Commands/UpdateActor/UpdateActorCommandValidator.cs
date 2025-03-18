using FluentValidation;

namespace WebApi.Applications.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator: AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x=> x.ActorId).GreaterThan(0).NotEmpty();
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
        }
    }
}