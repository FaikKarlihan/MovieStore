using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesViewModel Model { get; set; }
        public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetMoviesViewModel> Handle()
        {
            var movies = _context.Movies
                .Where(x => x.IsActive)
                .Include(x => x.Director)   
                .Include(x => x.Genre)      
                .Include(x => x.Actors)     
                .OrderBy(x => x.Id)
                .ToList();
            if (movies.Count<1)
                throw new InvalidOperationException("No movies found");

            List<GetMoviesViewModel> vm = _mapper.Map<List<GetMoviesViewModel>>(movies);

            return vm;
        }
    }
    public class GetMoviesViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public List<string> Actors{ get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
    }
}