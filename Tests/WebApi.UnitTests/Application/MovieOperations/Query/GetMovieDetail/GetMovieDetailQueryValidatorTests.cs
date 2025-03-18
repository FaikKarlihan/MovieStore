using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Queries.GetMovieDetail;
using Xunit;

namespace Tests.Application.MovieOperations.Query
{
    public class GetMovieDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = 1;
            // When
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
            query.MovieId = 0;
            // When
            GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}