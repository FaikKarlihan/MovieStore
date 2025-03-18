using FluentValidation;

namespace WebApi.Applications.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidator: AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
        }
    }
}