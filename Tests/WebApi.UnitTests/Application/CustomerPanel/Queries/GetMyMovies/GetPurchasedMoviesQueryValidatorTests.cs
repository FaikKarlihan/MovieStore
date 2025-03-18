using FluentAssertions;
using TestSetup;
using WepApi.Applications.CustomerPanel.Queries.GetMyMovies;
using Xunit;

namespace Tests.Application.CustomerPanel.Queries.GetMyMovies
{
    public class GetMyMoviesQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidEmailisGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetMyMoviesQuery query = new GetMyMoviesQuery(null, null);
            query.CustomerEmail = "test";
            // When
            GetMyMoviesQueryValidator validator = new GetMyMoviesQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }
    
        [Fact]
        public void WhenInvalidEmailisGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetMyMoviesQuery query = new GetMyMoviesQuery(null, null);
            query.CustomerEmail = "";
            // When
            GetMyMoviesQueryValidator validator = new GetMyMoviesQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }    
    }
}