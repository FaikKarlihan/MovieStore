using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.CreateCustomer;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "Tony",
                Surname = "Montana",
                Email = "scr@gmail.com",
                Password = "eyesneverlie"
            };
            command.Model = model;
            // When
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "",
                Surname = "",
                Email = "12345",
                Password = "12345"
            };
            command.Model = model;
            // When
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(4);
        }
    }
}