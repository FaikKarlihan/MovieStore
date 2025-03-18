using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.DeleteDirector;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.DeleteDirector
{
    public class DeleteDirectorCommmandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 1;
            // When
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShoulBeReturnError()
        {
            // Given
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            command.DirectorId = 0;
            // When
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}