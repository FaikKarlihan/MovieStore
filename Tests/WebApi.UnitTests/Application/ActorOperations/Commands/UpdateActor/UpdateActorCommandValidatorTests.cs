using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.UpdateActor;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateActorCommand command = new UpdateActorCommand(null, null);
            UpdateActorModel model = new UpdateActorModel()
            {
                Name = "asd",
                Surname = "dsa"
            };
            command.Model = model;
            command.ActorId = 1;
            // When
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("","a",1)]
        [InlineData("a","",1)]
        [InlineData("a","a",0)]
        [InlineData("","",0)]
        [InlineData("a","",0)]
        [InlineData("","",1)]
        [InlineData("","a",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string name, string surname, int id)
        {
            // Given
            UpdateActorCommand command = new UpdateActorCommand(null, null);
            UpdateActorModel model = new UpdateActorModel()
            {
                Name = name,
                Surname = surname
            };
            command.Model = model;
            command.ActorId = id;
            // When
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}