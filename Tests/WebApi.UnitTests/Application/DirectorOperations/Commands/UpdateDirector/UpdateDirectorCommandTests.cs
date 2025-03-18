using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Commands.UpdateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.DirectorOperation.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            // Given
            var director = new Director
            {
                Name = "Martin",
                Surname = "Scorsese"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "Stanley",
                Surname = "Kubrick"
            };
            command.Model = model;
            command.DirectorId = director.Id;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Directors.SingleOrDefault(x=> x.Id == director.Id);

            result.Should().NotBeNull();
            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Directors.Count()+99;
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "Stanley",
                Surname = "Kubrick"
            };
            command.Model = model;
            command.DirectorId = invalidId;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director not found");
        }

        [Fact]
        public void WhenAlreadyExistsDirectorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var director = new Director
            {
                Name = "Martin",
                Surname = "Scorsese"
            };
            _context.Directors.Add(director);
            _context.SaveChanges();

            UpdateDirectorCommand command = new UpdateDirectorCommand(_context, _mapper);
            UpdateDirectorModel model = new UpdateDirectorModel()
            {
                Name = "Martin",
                Surname = "Scorsese"
            };
            command.Model = model;
            command.DirectorId = director.Id;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director already exists");
        }
    }
}