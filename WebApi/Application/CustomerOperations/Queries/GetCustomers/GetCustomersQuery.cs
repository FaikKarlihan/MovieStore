using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomersViewModel Model { get; set; }
        public GetCustomersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GetCustomersViewModel> Handle()
        {
            var customers = _context.Customers
                .Include(x=> x.PurchasedMovies)
                .Include(x=> x.FavouriteGenres)
                .Where(x=> x.IsActive).OrderBy(x=> x.Id).ToList();

            if(customers.Count<1)
                throw new InvalidOperationException("No customers found");
            
            List<GetCustomersViewModel> vm = _mapper.Map<List<GetCustomersViewModel>>(customers);
            return vm;
        }
    }
    public class GetCustomersViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<string> PurchasedMovies { get; set; }
        public List<string> FavoriteGenres { get; set; }
    }
}