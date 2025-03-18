using FluentValidation;

namespace WebApi.Applications.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator: AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
        }
    }
}