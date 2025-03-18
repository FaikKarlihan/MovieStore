using FluentValidation;

namespace WebApi.Applications.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x=> x.Model.Name).NotEmpty();
            RuleFor(x=> x.Model.Surname).NotEmpty();
            RuleFor(x=> x.Model.Email).NotEmpty().MinimumLength(6);
            RuleFor(x=> x.Model.Password).NotEmpty().MinimumLength(6);
        }
    }
}