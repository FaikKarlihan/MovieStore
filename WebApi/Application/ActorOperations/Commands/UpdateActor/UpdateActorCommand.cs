using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }
        public UpdateActorModel Model;
        public UpdateActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var actor = _context.Actors.SingleOrDefault(x=> x.Id == ActorId);
            if(actor is null)
                throw new InvalidOperationException("Actor not found");
            if (_context.Actors.Any(x=> x.Name == Model.Name && x.Surname == Model.Surname))
                throw new InvalidOperationException("Actor already exists");
            
            _mapper.Map(Model, actor);
            _context.SaveChanges();
        }
    }
    
    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}