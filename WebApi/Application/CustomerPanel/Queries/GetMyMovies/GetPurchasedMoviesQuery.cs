using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WepApi.Applications.CustomerPanel.Queries.GetMyMovies
{
    public class GetMyMoviesQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public string CustomerEmail { get; set; }
        public MyMoviesViewModel Model { get; set; }
        public GetMyMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public MyMoviesViewModel Handle()
        {
            var customer = _context.Customers
                .Include(x => x.PurchasedMovies)
                .FirstOrDefault(x => x.Email == CustomerEmail);

            if (customer is null)
                throw new InvalidOperationException("Customer not found.");

            if (!customer.PurchasedMovies.Any())
                throw new InvalidOperationException("No Purchased movie found for this customer.");
                
            MyMoviesViewModel vm = _mapper.Map<MyMoviesViewModel>(customer);
            return vm;            
        }
    }
    public class MyMoviesViewModel
    {
        public List<string> PurchasedMovies { get; set; }
    }
}