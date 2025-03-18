using FluentAssertions;
using TestSetup;
using WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres;
using Xunit;

namespace Tests.Application.CustomerPanel.Queries.GetMyFavoriteGenres
{
    public class GetFavoriteGenresQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(null, null);
            query.CustomerEmail = "test";
            // When
            GetMyFavoriteGenresQueryValidator validator = new GetMyFavoriteGenresQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(null, null);
            query.CustomerEmail = "";
            // When
            GetMyFavoriteGenresQueryValidator validator = new GetMyFavoriteGenresQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}