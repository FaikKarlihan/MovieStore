using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Queries.GetOrderDetail;
using Xunit;

namespace Tests.Application.OrderOperations.Query
{
    public class GetOrderDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = 1;
            // When
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
            query.OrderId = 0;
            // When
            GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}