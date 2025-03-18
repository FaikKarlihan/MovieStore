using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.CreateDirector;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.CreateDirector
{
    public class CreateDirectorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
        {
            // Given
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "Quentin",
                Surname = "Tarantino"
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var director = _context.Directors.SingleOrDefault(x=> x.Name == model.Name);
            
            director.Surname.Should().Be(model.Surname);
        }
        
        [Fact]
        public void WhenAlreadyExistsDirectorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateDirectorCommand command = new CreateDirectorCommand(_context, _mapper);
            CreateDirectorModel model = new CreateDirectorModel()
            {
                Name = "Jonathan",
                Surname = "Demme"
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director already exists");
        }
    }
}