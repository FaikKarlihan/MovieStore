using System;
using WebApi.DBOperations;

namespace WebApi.Applications.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int MovieId { get; set; }
        public DeleteMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var movie = _context.Movies.Find(MovieId); 
            if(movie is null)
                throw new InvalidOperationException("Movie not found");
            if (!movie.IsActive)
                throw new InvalidOperationException("Movie is already inactive");
            
            movie.IsActive=false;
            _context.SaveChanges();
        }
    }
}