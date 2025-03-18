using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Customers
    {
        public static void AddCustomers(this MovieStoreDbContext context)
        {
            var movie1 = context.Movies.SingleOrDefault(x => x.Id == 1);
            var crimeGenre = context.Genres.SingleOrDefault(x => x.Name == "Crime");
            var thrillerGenre = context.Genres.SingleOrDefault(x => x.Name == "Thriller");
            
            context.Customers.AddRange
            (
                new Customer
                {
                    Name = "Clarice",
                    Surname = "Starling",
                    Email = "clr@gmail.com",
                    Password = "123456",
                    FavouriteGenres = new List<Genre> { crimeGenre, thrillerGenre },
                    PurchasedMovies = new List<Movie> { movie1 }
                }                
            );
        }
    }
}