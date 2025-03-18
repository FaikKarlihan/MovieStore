using System;
using System.Collections.Generic;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.CreateMovie;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "Kill Bill Volume 1",
                Director = "Quentin Tarantino",
                Genre = "Thriller",
                Actors =  new List<string> { "Uma Thurman" },
                Price = 21,
                PublishDate = new DateTime(2004, 1, 2)
            };
            command.Model = model;
            // When
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }
        
        [Theory]
        [InlineData("", "", "", "", 0)]
        [InlineData("a", "a a", "a", "a a", 1)]
        [InlineData("", "a a", "a", "a a", 1)]
        [InlineData("a", "", "a", "a a", 1)]
        [InlineData("a", "a", "", "a", 0)]
        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError
        (string name, string director, string genre, string actor, int price)

        {
            // Given
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = name,
                Director = director,
                Genre = genre,
                Actors =  new List<string> { actor },
                Price = price,
                PublishDate = new DateTime(0001, 01, 01)
            };
            command.Model = model;
            // When
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}