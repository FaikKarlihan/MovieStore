using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            // Given
            var actor = new Actor
            {
                Name = "Katy",
                Surname = "Perry"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            UpdateActorModel model = new UpdateActorModel()
            {
                Name = "White",
                Surname = "Buffalo"
            };
            command.Model = model;
            command.ActorId = actor.Id;
            
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Actors.SingleOrDefault(x=> x.Id==actor.Id);

            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var actor = new Actor
            {
                Name = "Deep",
                Surname = "Purple"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            UpdateActorModel model = new UpdateActorModel()
            {
                Name = "Jodie",     // already exists in the in-memory database
                Surname = "Foster"
            };
            command.Model = model;
            command.ActorId = actor.Id;
            
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor already exists");
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Actors.Count()+99;

            UpdateActorCommand command = new UpdateActorCommand(_context, _mapper);
            command.ActorId = invalidId;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor not found");
        }
    }
}