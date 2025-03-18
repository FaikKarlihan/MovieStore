using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerPanel.Commands.BuyMovie;
using Xunit;

namespace Tests.Application.CustomerPanel.Commands.BuyMovie
{
    public class BuyMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            BuyMovieCommand command = new BuyMovieCommand(null);
            BuyMovieModel model = new BuyMovieModel()
            {
                Movie = "Fracture"
            };
            command.Model = model;
            command.Email = "clr@gmail.com";
            // When
            BuyMovieCommandValidator validator = new BuyMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShoulBeReturnError()
        {
            // Given
            BuyMovieCommand command = new BuyMovieCommand(null);
            BuyMovieModel model = new BuyMovieModel()
            {
                Movie = ""
            };
            command.Model = model;
            command.Email = "";
            // When
            BuyMovieCommandValidator validator = new BuyMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}