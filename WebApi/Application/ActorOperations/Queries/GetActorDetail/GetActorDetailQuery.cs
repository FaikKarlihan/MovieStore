using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int ActorId { get; set; }
        public GetActorDetailQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GetActorDetailModel Handle()
        {
            var actor = _context.Actors.Include(x=> x.Movies).Where(x=> x.Id == ActorId).SingleOrDefault();
                if (actor is null)
            throw new InvalidOperationException("Actor not found");

            GetActorDetailModel vm = _mapper.Map<GetActorDetailModel>(actor);
            return vm;
        }
    }

    public class GetActorDetailModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}
