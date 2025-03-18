using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.DeleteCustomer;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = 1;
            // When
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteCustomerCommand command = new DeleteCustomerCommand(null);
            command.CustomerId = 0;
            // When
            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}