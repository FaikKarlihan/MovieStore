using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Commands.CreateActor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            // Given
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            CreateActorModel model = new CreateActorModel()
            {
                Name = "Sagopa",
                Surname = "Kajmer"
            };  
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var actor = _context.Actors.SingleOrDefault(x=> x.Name==model.Name && x.Surname==model.Surname);

            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.Name);
            actor.Surname.Should().Be(model.Surname);
        }   
        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            CreateActorModel model = new CreateActorModel()
            {
                Name = "Jodie",
                Surname = "Foster"  // already exists in the in-memory database
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor already exists");   
        }
    }
}