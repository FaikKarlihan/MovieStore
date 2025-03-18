using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Commands.DeleteOrder;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.OrderId = 1;
            // When
            DeleteOrderCommandValdiator validator = new DeleteOrderCommandValdiator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteOrderCommand command = new DeleteOrderCommand(null);
            command.OrderId = 0;
            // When
            DeleteOrderCommandValdiator validator = new DeleteOrderCommandValdiator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}