using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.UpdateDirector;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "David",
                Surname = "Fincher"
            };
            command.Model = model;
            command.DirectorId = 1;
            // When
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "",
                Surname = ""
            };
            command.Model = model;
            command.DirectorId = 0;
            // When
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}