using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this MovieStoreDbContext context)
        {
            context.Directors.AddRange
            (
                new Director { Name = "Jonathan", Surname = "Demme" },
                new Director { Name = "Gregory", Surname = "Hoblit" }
            );
        }
    }
}