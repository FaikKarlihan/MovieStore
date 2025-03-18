using FluentValidation;

namespace WebApi.Applications.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator: AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(x=> x.ActorId).NotNull().GreaterThan(0);
        }
    }
}