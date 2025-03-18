using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Queries.GetCustomerDetail;
using Xunit;

namespace Tests.Application.CustomerOperations.Query
{
    public class GetCustomerDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 1;
            // When
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(null, null);
            query.CustomerId = 0;
            // When
            GetCustomerDetailQueryValidator validator = new GetCustomerDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}