using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.DeleteActor;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = 1;
            // When
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void WhenInvalidIdIsGiven_Validator_ShoulBeReturnError(int id)
        {
            // Given
            DeleteActorCommand command = new DeleteActorCommand(null);
            command.ActorId = id;
            // When
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}