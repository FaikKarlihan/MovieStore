using FluentValidation;

namespace WebApi.Applications.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x=> x.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}