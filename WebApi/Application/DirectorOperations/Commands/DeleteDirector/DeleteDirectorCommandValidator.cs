using FluentValidation;

namespace WebApi.Applications.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidator: AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(x=> x.DirectorId).NotNull().GreaterThan(0);
        }
    }
}