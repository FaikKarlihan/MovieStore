using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.MovieOperations.Commands.UpdateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Movie_ShouldBeUpdated()
        {
            // Given
            AddActor("Robert","De Niro");
            AddDirector("Martin", "Scorsese");

            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = "Taxi Driver",
                Director = "Martin Scorsese",
                Genre = "Drama",
                Actors =  new List<string> { "Robert De Niro" },
                Price = 25,
                PublishDate = new DateTime(1977, 12, 1),
                IsActive = true
            };
            command.Model = model;
            command.MovieId = 1;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var movie = _context.Movies.SingleOrDefault(x=> x.Id == 1);

            movie.Director.Name.Should().Be("Martin");
            movie.Genre.Name.Should().Be("Drama");
            movie.Name.Should().Be("Taxi Driver");
            movie.Actors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenInvalidGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            UpdateMovieCommand command = new UpdateMovieCommand(_context, _mapper);
            UpdateMovieModel model = new UpdateMovieModel()
            {
                Name = "Taxi Driver",
                Director = "Martin Scorsese",
                Genre = "test",
                Actors =  new List<string> { "Robert De Niro" },
                Price = 25,
                PublishDate = new DateTime(1977, 12, 1),
                IsActive = true
            };
            command.Model = model;
            command.MovieId = 1;            
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be($"No genre found with the name '{model.Genre}', please add it first.");
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