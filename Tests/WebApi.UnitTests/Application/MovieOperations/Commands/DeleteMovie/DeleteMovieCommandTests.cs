using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.DeleteMovie;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Movie_ShouldBeDeleted()
        {
            // Given
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = 1;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            var movie = _context.Movies.SingleOrDefault(x=> x.Id == 1);
            // Then
            movie.IsActive.Should().Be(false);

            movie.IsActive = true; // return to default
            _context.SaveChanges();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Movies.Count()+99;
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = invalidId;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie not found");
        }

        [Fact]
        public void WhenAlreadyDeletedMovieIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movie = _context.Movies.SingleOrDefault(x=> x.Id == 1);
            movie.IsActive = false;
            // Given
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = 1;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie is already inactive");

            movie.IsActive = true; // return to default
            _context.SaveChanges();
        }
    }
}