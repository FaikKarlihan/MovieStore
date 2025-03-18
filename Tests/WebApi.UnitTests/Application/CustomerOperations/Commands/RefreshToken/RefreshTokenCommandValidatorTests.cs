using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenTokenIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(null, null);
            command.refreshToken = "test";
            // When
            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenTokenIsNotGiven_Validator_ShouldBeReturnError()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(null, null);
            command.refreshToken = "";
            // When
            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}