using FluentValidation;

namespace WebApi.Applications.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderDetailQueryValidator: AbstractValidator<GetOrderDetailQuery>
    {
        public GetOrderDetailQueryValidator()
        {
            RuleFor(x=> x.OrderId).NotEmpty().GreaterThan(0);
        }
    }
}