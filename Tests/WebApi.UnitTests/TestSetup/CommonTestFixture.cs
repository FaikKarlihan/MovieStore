using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture : IDisposable
    {
        public MovieStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public CommonTestFixture()
        {
            ResetDatabase(); // Reset database for each new test

            Mapper = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            var configData = new Dictionary<string, string>
            {
                { "Token:SecurityKey", "TestSuperSecretKey123!" },
                { "Token:Issuer", "TestIssuer" },
                { "Token:Audience", "TestAudience" }
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();
        }

        private void ResetDatabase()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Separate DB for each test
                .Options;

            Context = new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();

            //
            Context.AddActors();
            Context.AddDirectors();
            Context.AddGenres();
            Context.SaveChanges();
            Context.AddMovies();
            Context.SaveChanges();     //SaveChanges was used as a simple solution for processing interconnected data
            Context.AddCustomers();
            Context.SaveChanges();
            Context.AddOrders();
            Context.AddLoggs();
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
