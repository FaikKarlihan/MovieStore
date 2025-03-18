using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Queries.GetDirectorDetail;
using Xunit;

namespace Tests.Application.DirectorOperation.Query
{
    public class GetDirectorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
            query.DirectorId = 1;
            // When
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
            query.DirectorId = 0;
            // When
            GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}