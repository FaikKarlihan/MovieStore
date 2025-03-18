using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this MovieStoreDbContext context)
        {
            context.Genres.AddRange
            (
                new Genre { Name = "Crime" },
                new Genre { Name = "Thriller" },
                new Genre { Name = "Drama" }
            );
        }
    }
}