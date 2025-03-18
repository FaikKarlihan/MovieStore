using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Loggs
    {
        public static void AddLoggs(this MovieStoreDbContext context)
        {
            context.DatabaseLoggs.AddRange
            (
                new DbLoggs { LogMessage = "Test"}
            );
        }
    }
}