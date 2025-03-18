using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.CustomerPanel.Commands.BuyMovie
{
    public class BuyMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        public string Email { get; set; }
        public BuyMovieModel Model { get; set; }
        public BuyMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var customer = _context.Customers
                .Include(x=> x.PurchasedMovies)
                .SingleOrDefault(x=> x.Email == Email);
            if(customer is null)
                throw new InvalidOperationException("Customer not found");

            var movie = _context.Movies.SingleOrDefault(x=> x.Name.ToLower() == Model.Movie.ToLower());
            if(movie is null)
                throw new InvalidOperationException("Movie not found");

            var order = new Order
            {
                Customer = customer,
                Movie = movie,
                OrderDate = DateTime.Now,
                TotalPrice = movie.Price
            };
            _context.Orders.Add(order);

            customer.PurchasedMovies.Add(movie);
            _context.SaveChanges();
        }
    }
    public class BuyMovieModel
    {
        public string Movie { get; set; }
    }
}