using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.CreateDirector;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.CreateDirector
{
    public class CreateDirectorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "Andrey",
                Surname = "Tarkovski"
            };
            command.Model = model;
            // When
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "",
                Surname = ""
            };
            command.Model = model;
            // When
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}