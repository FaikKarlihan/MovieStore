using FluentValidation;

namespace WebApi.Applications.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator: AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(x=> x.DirectorId).GreaterThan(0).NotEmpty();
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
        }
    }
}