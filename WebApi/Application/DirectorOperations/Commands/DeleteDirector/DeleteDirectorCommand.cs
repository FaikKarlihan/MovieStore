using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DirectorId { get; set; }
        public DeleteDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.Include(x=> x.Movies).SingleOrDefault(x=> x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Director not found");
            
            if (director.Movies.Any())
                throw new InvalidOperationException("A director who directed any movie cannot be deleted");
            
            _context.Directors.Remove(director);
            _context.SaveChanges();
        }
    }
}