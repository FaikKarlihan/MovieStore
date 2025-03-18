using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorsViewModel Model;
        public GetDirectorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetDirectorsViewModel> Handle()
        {
            var directors = _context.Directors.Include(x=> x.Movies).OrderBy(x=> x.Id).ToList();
            List<GetDirectorsViewModel> vm = _mapper.Map<List<GetDirectorsViewModel>>(directors);
            return vm;
        }
    }

    public class GetDirectorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}