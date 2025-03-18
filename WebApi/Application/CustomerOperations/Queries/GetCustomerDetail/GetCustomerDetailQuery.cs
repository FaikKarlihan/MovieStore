using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailViewModel Model { get; set; }
        public int CustomerId { get; set; }
        public GetCustomerDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetCustomerDetailViewModel Handle()
        {
            var customer = _context.Customers
                .Include(x=> x.PurchasedMovies)
                .Include(x=> x.FavouriteGenres)
                .SingleOrDefault(x=> x.Id == CustomerId && x.IsActive);

            if(customer is null)
                throw new InvalidOperationException("Customer not found");
            
            GetCustomerDetailViewModel vm = _mapper.Map<GetCustomerDetailViewModel>(customer);
            return vm;
        }
    }
    public class GetCustomerDetailViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<string> PurchasedMovies { get; set; }
        public List<string> FavoriteGenres { get; set; }
    }
}