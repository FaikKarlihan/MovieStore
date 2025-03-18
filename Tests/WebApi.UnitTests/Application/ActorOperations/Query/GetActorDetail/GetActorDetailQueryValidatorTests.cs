using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Queries.GetActorDetail;
using Xunit;

namespace Tests.Application.ActorOperations.Query
{
    public class GetActorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.ActorId = 1;
            // When
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            GetActorDetailQuery query = new GetActorDetailQuery(null, null);
            query.ActorId = 0;
            // When
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            var result = validator.Validate(query);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}