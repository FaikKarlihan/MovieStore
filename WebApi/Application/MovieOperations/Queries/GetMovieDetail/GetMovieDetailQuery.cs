using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int MovieId { get; set; }
        public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetMovieDetailViewModel Handle()
        {
            var movie = _context.Movies
                .Where(x => x.IsActive)
                .Include(x => x.Director)   
                .Include(x => x.Genre)      
                .Include(x => x.Actors)     
                .SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("Movie not found");

            GetMovieDetailViewModel vm = _mapper.Map<GetMovieDetailViewModel>(movie);
            return vm;
        }
    }

    public class GetMovieDetailViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public List<string> Actors{ get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }        
    }
}
