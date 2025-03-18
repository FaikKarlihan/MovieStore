using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.CreateActor;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateActorCommand command = new CreateActorCommand(null, null);    
            CreateActorModel model = new CreateActorModel()
            {
                Name = "notnull",
                Surname = "notnull"
            };
            command.Model = model;
            // When
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            CreateActorCommand command = new CreateActorCommand(null, null);    
            CreateActorModel model = new CreateActorModel()
            {
                Name = "",
                Surname = ""
            };
            command.Model = model;
            // When
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}