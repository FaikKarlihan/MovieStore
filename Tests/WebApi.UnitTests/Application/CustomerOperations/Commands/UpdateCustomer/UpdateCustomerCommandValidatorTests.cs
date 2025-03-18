using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.UpdateCustomer;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateCustomerCommand command = new UpdateCustomerCommand(null);
            UpdateCustomerModel model = new UpdateCustomerModel()
            {
                Name = "test",
                Surname = "test",
                Email = "test",
                Password = "test",
                IsActive = true
            };
            command.Model = model;
            command.CustomerId = 1;
            // When
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(command);

            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            UpdateCustomerCommand command = new UpdateCustomerCommand(null);
            UpdateCustomerModel model = new UpdateCustomerModel()
            {
                Name = "test",
                Surname = "test",
                Email = "test",
                Password = "test",
                IsActive = true
            };
            command.Model = model;
            command.CustomerId = 0;
            // When
            UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(command);

            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}