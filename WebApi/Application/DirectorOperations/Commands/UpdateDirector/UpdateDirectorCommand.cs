using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model;
        public UpdateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x=> x.Id == DirectorId);
            if(director is null)
                throw new InvalidOperationException("Director not found");
            if (_context.Directors.Any(x=> x.Name == Model.Name && x.Surname == Model.Surname))
                throw new InvalidOperationException("Director already exists");
            
            _mapper.Map(Model, director);
            _context.SaveChanges();
        }
    }
    
    public class UpdateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}