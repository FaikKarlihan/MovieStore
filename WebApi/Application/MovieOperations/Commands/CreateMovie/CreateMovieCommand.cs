using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieModel Model { get; set; }
        public CreateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var movie = _context.Movies
                .Include(x => x.Director)
                .Include(x => x.Actors)
                .Include(x => x.Genre)
                .SingleOrDefault(x => x.Name == Model.Name);
            if(movie is not null)
                throw new InvalidOperationException("Movie already exists");
            
            var genre = GetGenreFromDatabase();
            var director = GetDirectorFromDatabase();
            var actors = GetActorsFromDatabase(); 

            movie = _mapper.Map<Movie>(Model);
            movie.Director = director;
            movie.Genre = genre;
            movie.Actors = actors;

            _context.Movies.Add(movie);
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
            var genre = _context.Genres.SingleOrDefault(x=> x.Name.ToLower() == Model.Genre.ToLower());
            if (genre is null)
                throw new InvalidOperationException("No genre found with this name, please add genre first");
            return genre;
        }
    }
    public class CreateMovieModel
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public List<string> Actors { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
    }
}