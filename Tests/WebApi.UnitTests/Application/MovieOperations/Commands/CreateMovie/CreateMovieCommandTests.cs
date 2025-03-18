using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.CreateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeCreated()
        {
            // Given
            AddDirector("Quentin","Tarantino");
            AddActor("Uma","Thurman");

            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "Kill Bill Volume 1",
                Director = "Quentin Tarantino",
                Genre = "Thriller",
                Actors =  new List<string> { "Uma Thurman" },
                Price = 21,
                PublishDate = new DateTime(2004, 1, 2)
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var movie = _context.Movies.SingleOrDefault(x=> x.Name == model.Name);

            movie.Should().NotBeNull();
            movie.Director.Name.Should().Be("Quentin");
            movie.Director.Surname.Should().Be("Tarantino");
            movie.Genre.Name.Should().Be("Thriller");
            movie.Price.Should().Be(model.Price);
        }

        [Fact]
        public void WhenInvalidDirectorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "test",
                Director = "test test",
                Genre = "Thriller",
                Actors =  new List<string> { "Uma Thurman" },
                Price = 21,
                PublishDate = new DateTime(2004, 1, 2)
            };
            command.Model = model;
            string firstName = "test", lastName = "test";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be($"Director '{firstName} {lastName}' not found. Please add the director first.");
        }

        [Fact]
        public void WhenInvalidActorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "test",
                Director = "Quentin Tarantino",
                Genre = "Thriller",
                Actors =  new List<string> { "test test" },
                Price = 21,
                PublishDate = new DateTime(2004, 1, 2)
            };
            command.Model = model;
            string firstName = "test", lastName = "test";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be($"Actor '{firstName} {lastName}' not found. Please add the actor first.");
        }

        [Fact]
        public void WhenInvalidGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            CreateMovieModel model = new CreateMovieModel()
            {
                Name = "test",
                Director = "Quentin Tarantino",
                Genre = "test",
                Actors =  new List<string> { "Uma Thurman" },
                Price = 21,
                PublishDate = new DateTime(2004, 1, 2)
            };
            command.Model = model;

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("No genre found with this name, please add genre first");
        }

        public void AddDirector(string name, string surname)
        {
            var director = new Director
            {
                Name = name,
                Surname = surname
            };
            _context.Directors.Add(director);
            _context.SaveChanges();
        }
        public void AddActor(string name, string surname)
        {
            var actor = new Actor
            {
                Name = name,
                Surname = surname
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }
}