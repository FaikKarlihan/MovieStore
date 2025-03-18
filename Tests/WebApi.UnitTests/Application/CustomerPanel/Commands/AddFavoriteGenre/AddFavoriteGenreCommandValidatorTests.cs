using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerPanel.Commands.AddFavoriteGenre;
using Xunit;

namespace Tests.Application.CustomerPanel.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(null);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = "test"
            };
            command.Email = "test";
            command.Model = model;
            // When
            AddFavoriteGenreCommandValidator validator = new AddFavoriteGenreCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError()
        {
            // Given
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(null);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = ""
            };
            command.Email = "";
            command.Model = model;
            // When
            AddFavoriteGenreCommandValidator validator = new AddFavoriteGenreCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}