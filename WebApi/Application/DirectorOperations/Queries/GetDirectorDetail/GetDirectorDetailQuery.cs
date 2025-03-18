using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }
        public GetDirectorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetDirectorDetailViewModel Handle()
        {
            var director = _context.Directors.Include(x=> x.Movies).Where(x=> x.Id == DirectorId).SingleOrDefault();
                if (director is null)
            throw new InvalidOperationException("Director not found");

            GetDirectorDetailViewModel vm = _mapper.Map<GetDirectorDetailViewModel>(director);
            return vm;
        }
    }

    public class GetDirectorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}
