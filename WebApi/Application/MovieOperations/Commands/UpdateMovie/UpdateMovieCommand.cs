using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }
        public UpdateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var movie = _context.Movies
                .Include(x=> x.Director)
                .Include(x=> x.Genre)
                .Include(x=> x.Actors)
                .SingleOrDefault(x=> x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Movie not found");

            _mapper.Map(Model, movie);

movie.Name = !string.IsNullOrWhiteSpace(Model.Name) && Model.Name != default ? Model.Name : movie.Name;
movie.Price = Model.Price != default ? Model.Price : movie.Price;
movie.Genre = !string.IsNullOrWhiteSpace(Model.Genre) && Model.Genre != default ? GetGenreFromDatabase() : movie.Genre;
movie.Director = !string.IsNullOrWhiteSpace(Model.Director) && Model.Director != default ? GetDirectorFromDatabase() : movie.Director;
movie.Actors = (Model.Actors != null && Model.Actors.Any()) ? GetActorsFromDatabase() : movie.Actors;


            _context.SaveChanges();
        }
        public List<Actor> GetActorsFromDatabase()
        {
            List<Actor> actors = new List<Actor>();

            foreach (var actorFullName in Model.Actors)
            {
                // We separate the first and last names
                var nameParts = actorFullName.Split(' ');

                if (nameParts.Length < 2)
                    throw new InvalidOperationException($"Invalid actor name format: {actorFullName}. Please provide 'Name Surname'.");

                string firstName = nameParts[0];
                string lastName = string.Join(" ", nameParts.Skip(1)); // The surname is the remaining part.

                // Find actor in database
                var actor = _context.Actors
                    .SingleOrDefault(a => a.Name.ToLower() == firstName.ToLower() && a.Surname.ToLower() == lastName.ToLower());

                if (actor is null)
                    throw new InvalidOperationException($"Actor '{firstName} {lastName}' not found. Please add the actor first.");

                actors.Add(actor);
            }
            return actors;
        }
        public Director GetDirectorFromDatabase()
        {
            var nameParts = Model.Director.Split(' ');
            if (nameParts.Length < 2)
                throw new InvalidOperationException($"Invalid director name format: {Model.Director}. Please provide 'Name Surname'.");

            string firstName = nameParts[0];
            string lastName = string.Join(" ", nameParts.Skip(1));

            var director = _context.Directors
                .SingleOrDefault(a => a.Name.ToLower() == firstName.ToLower() && a.Surname.ToLower() == lastName.ToLower());
            if (director is null)
                throw new InvalidOperationException($"Director '{firstName} {lastName}' not found. Please add the director first.");

            return director;
        }
        public Genre GetGenreFromDatabase()
        {
            var genre = _context.Genres
                .SingleOrDefault(x => string.Equals(x.Name, Model.Genre, StringComparison.OrdinalIgnoreCase));

            if (genre is null)
                throw new InvalidOperationException($"No genre found with the name '{Model.Genre}', please add it first.");

            return genre;
        }

    }
    public class UpdateMovieModel
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public List<string> Actors { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
    }
}