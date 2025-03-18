using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.DeleteDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.DeleteDirector
{
    public class DeleteDirectorCommmandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommmandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenValidIdIsGiven_Director_ShouldBeDeleted()
        {
            // Given
            var director = new Director
            {
                Name = "Christopher",
                Surname = "Nolan"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = director.Id;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Directors.SingleOrDefault(x=> x.Id == director.Id);
            
            result.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Directors.Count()+99;

            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = invalidId;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Director not found");
        }

        [Fact]
        public void WhenADirectorWhoDirectedAMovieIsTriedToBeDeleted_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = 1; // id is a director in a movie
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("A director who directed any movie cannot be deleted");
        }
    }
}