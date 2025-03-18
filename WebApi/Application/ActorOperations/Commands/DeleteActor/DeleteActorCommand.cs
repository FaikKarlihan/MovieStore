using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int ActorId { get; set; }
        public DeleteActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var actor = _context.Actors.Include(x=> x.Movies).SingleOrDefault(x=> x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("Actor not found");
            
            if (actor.Movies.Any())
                throw new InvalidOperationException("An actor who plays in any movie cannot be deleted");
            
            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }
    }
}