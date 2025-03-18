using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.DeleteActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Fact]
        public void WhenValidIdIsGiven_Actor_ShouldBeDeleted()
        {
            // Given
            var actorr = new Actor
            {
                Name = "test",
                Surname = "test"
            };
            _context.Actors.Add(actorr);
            _context.SaveChanges();

            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = actorr.Id;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var actor = _context.Actors.SingleOrDefault(x=> x.Id==actorr.Id);

            actor.Should().BeNull();
        }   

        [Fact]
        public void WhenAnActorINaMovieIsTriedToBeDeleted_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = 1; // actor id playing in a movie
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("An actor who plays in any movie cannot be deleted");
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int invalidId = _context.Actors.Count()+99;
            // Given
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = invalidId;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor not found");
        }
    }
}