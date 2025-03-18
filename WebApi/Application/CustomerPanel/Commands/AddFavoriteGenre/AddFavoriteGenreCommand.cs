using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.CustomerPanel.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommand
    {
        private readonly IMovieStoreDbContext _context;
        public string Email { get; set; }
        public AddFavoriteGenreModel Model { get; set; }
        public AddFavoriteGenreCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers
                .Include(x=> x.FavouriteGenres)
                .SingleOrDefault(x=> x.Email == Email);
            if(customer is null)
                throw new InvalidOperationException("Customer not found");

            var genre = _context.Genres.SingleOrDefault(x=> x.Name.ToLower() == Model.Genre.ToLower());
            if(genre is null)
                throw new InvalidOperationException("Genre not found");
            
            if(customer.FavouriteGenres.Contains(genre))
                throw new InvalidOperationException("Genre is already in favorites");

            customer.FavouriteGenres.Add(genre);
            _context.SaveChanges();
        }   
    }
    public class AddFavoriteGenreModel
    {
        public string Genre { get; set; }
    }
}