using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres
{
    public class GetMyFavoriteGenresQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public string CustomerEmail { get; set; }
        public FavoriteGenresViewModel Model { get; set; }
        public GetMyFavoriteGenresQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public FavoriteGenresViewModel Handle()
        {
            var customer = _context.Customers
                .Include(x => x.FavouriteGenres)
                .FirstOrDefault(x => x.Email == CustomerEmail);

            if (customer is null)
                throw new InvalidOperationException("Customer not found.");

            if (!customer.FavouriteGenres.Any())
                throw new InvalidOperationException("No favorite genres found for this customer.");

            FavoriteGenresViewModel vm = _mapper.Map<FavoriteGenresViewModel>(customer);
            return vm;
        }
    }
    public class FavoriteGenresViewModel
    {
        public List<string> FavoriteGenres { get; set; }
    }
}