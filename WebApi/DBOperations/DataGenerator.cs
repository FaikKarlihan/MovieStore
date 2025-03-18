using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }

                context.Actors.AddRange(
                    new Actor { Name = "Jodie", Surname = "Foster" },
                    new Actor { Name = "Anthony", Surname = "Hopkins" }
                );



                context.Directors.AddRange(
                    new Director { Name = "Jonathan", Surname = "Demme" }
                );



                context.Genres.AddRange(
                    new Genre { Name = "Crime" },
                    new Genre { Name = "Thriller" },
                    new Genre { Name = "Drama" }
                );
                context.SaveChanges();


                var jodie = context.Actors.SingleOrDefault(x => x.Name == "Jodie" && x.Surname == "Foster");
                var anthony = context.Actors.SingleOrDefault(x => x.Name == "Anthony" && x.Surname == "Hopkins");
                var crimeGenre = context.Genres.SingleOrDefault(x => x.Name == "Crime");

                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "The Silence of the Lambs",
                        PublishDate = new DateTime(1991, 2, 14),
                        Price = 20.99,
                        IsActive = true,
                        Genre = crimeGenre,
                        DirectorId = 1,
                        Actors = new List<Actor> { jodie, anthony }
                    }
                );
                context.SaveChanges();

                
                var movie1 = context.Movies.SingleOrDefault(x => x.Id == 1);
                
                var thrillerGenre = context.Genres.SingleOrDefault(x => x.Name == "Thriller");

                var customer = new Customer
                {
                    Name = "Clarice",
                    Surname = "Starling",
                    Email = "clr@gmail.com",
                    Password = "123456",
                    FavouriteGenres = new List<Genre> { crimeGenre, thrillerGenre },
                    PurchasedMovies = new List<Movie> { movie1 }
                };

                context.Customers.Add(customer);
                context.SaveChanges();


                
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    TotalPrice = movie1.Price,
                    CustomerId = customer.Id,
                    MovieId = movie1.Id 
                };

                context.Orders.Add(order);
                context.SaveChanges();


            }
        }
    }
}