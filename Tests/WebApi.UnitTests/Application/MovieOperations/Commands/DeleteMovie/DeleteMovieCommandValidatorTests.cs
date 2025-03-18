using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.DeleteMovie;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = 1;
            // When
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId = 0;
            // When
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}