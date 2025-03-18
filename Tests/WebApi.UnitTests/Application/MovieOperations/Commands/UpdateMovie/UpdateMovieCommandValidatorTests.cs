using System;
using System.Collections.Generic;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.UpdateMovie;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateMovieCommand command = new UpdateMovieCommand(null, null);
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = "Taxi Driver",
                Director = "Martin Scorsese",
                Genre = "Drama",
                Actors =  new List<string> { "Robert De Niro" },
                Price = 25,
                PublishDate = new DateTime(1977, 12, 1),
                IsActive = true                
            };
            command.Model = model;
            command.MovieId = 1;
            // When
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            UpdateMovieCommand command = new UpdateMovieCommand(null, null);
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = "Taxi Driver",
                Director = "Martin Scorsese",
                Genre = "Drama",
                Actors =  new List<string> { "Robert De Niro" },
                Price = 25,
                PublishDate = new DateTime(1977, 12, 1),
                IsActive = true                
            };
            command.Model = model;
            command.MovieId = 0;
            // When
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}