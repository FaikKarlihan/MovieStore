using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Queries.GetMovieDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.MovieOperations.Query
{
    public class GetMovieDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Movie_ShouldBeReturn()
        {
            // Given
            GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
            query.MovieId = 1;
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            result.Should().NotBeNull();
            result.Actors.Count.Should().Be(2);
            result.Name.Should().Be("The Silence of the Lambs"); 
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Movies.Count()+99;

            GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
            query.MovieId = invalidId;
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Movie not found");
        }
    }
}