using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.DbLoggsOperations
{
    public class GetDbLoggsQuery
    {
        private readonly IMovieStoreDbContext _context;
        public GetDbLoggsViewModel Model { get; set; }
        public IMapper _mapper;
        public GetDbLoggsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GetDbLoggsViewModel> Handle()
        {
            var loggs = _context.DatabaseLoggs.OrderBy(x=> x.Id).ToList();

            List<GetDbLoggsViewModel> vm = _mapper.Map<List<GetDbLoggsViewModel>>(loggs);
            return vm;
        }
    }
    public class GetDbLoggsViewModel
    {
        public string LogMessage { get; set; }
    }
}