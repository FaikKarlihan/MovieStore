using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "clr@gmail.com",
                Password = "123456"
            };
            command.Model = model;
            // When
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(null, null);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "",
                Password = ""
            };
            command.Model = model;
            // When
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}