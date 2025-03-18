using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DbLoggsOperations
{
    public class DbLoggerCommand
    {
        private readonly IMovieStoreDbContext _context;
        public string log { get; set; }
        public DbLoggerCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(string log)
        {
            var dblog = new DbLoggs
            {
                LogMessage = log
            };
            _context.DatabaseLoggs.Add(dblog);
            _context.SaveChanges();
        }
    }
}