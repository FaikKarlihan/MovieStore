using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this MovieStoreDbContext context)
        {
            var jodie = context.Actors.SingleOrDefault(x => x.Name == "Jodie" && x.Surname == "Foster");
            var anthony = context.Actors.SingleOrDefault(x => x.Name == "Anthony" && x.Surname == "Hopkins");
            var crimeGenre = context.Genres.SingleOrDefault(x => x.Name == "Crime");
            var thrillerGenre = context.Genres.SingleOrDefault(x=> x.Name == "Thriller");
            
            context.Movies.AddRange
            (
                new Movie
                {
                    Name = "The Silence of the Lambs",
                    PublishDate = new DateTime(1991, 2, 14),
                    Price = 20.99,
                    IsActive = true,
                    Genre = crimeGenre,
                    DirectorId = 1,
                    Actors = new List<Actor> { jodie, anthony }
                },

                new Movie
                {
                    Name = "Fracture",
                    PublishDate = new DateTime(2007, 4, 20),
                    Price = 15.99,
                    IsActive = true,
                    Genre = thrillerGenre,
                    DirectorId = 2,
                    Actors = new List<Actor> { anthony }
                }
            );
        } // film ekle ki adam satın alsın
    }
}