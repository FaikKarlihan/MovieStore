using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorsViewModel Model;
        public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetActorsViewModel> Handle()
        {
            var actors = _context.Actors.Include(x=> x.Movies).OrderBy(x=> x.Id).ToList();
            List<GetActorsViewModel> vm = _mapper.Map<List<GetActorsViewModel>>(actors);
            return vm;
        }
    }

    public class GetActorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}