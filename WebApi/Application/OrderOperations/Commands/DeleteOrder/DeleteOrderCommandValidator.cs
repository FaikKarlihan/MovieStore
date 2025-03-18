using FluentValidation;

namespace WebApi.Applications.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandValdiator: AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValdiator()
        {
            RuleFor(x=> x.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}