using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.DbLoggsOperations;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class LoggsController: ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public LoggsController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("Get-Db-Loggs")]
        public ActionResult GetDbLoggs()
        {
            GetDbLoggsQuery query = new GetDbLoggsQuery(_context, _mapper);
            
            var obj = query.Handle();
            return Ok(obj);
        }
    }
}