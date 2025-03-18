using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using WepApi.Applications.CustomerPanel.Queries.GetMyFavoriteGenres;
using Xunit;

namespace Tests.Application.CustomerPanel.Queries.GetMyFavoriteGenres
{
    public class GetFavoriteGenresQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetFavoriteGenresQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidEmailIsGiven_Genres_ShouldBeReturn()
        {
            // Given
            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(_context, _mapper);
            query.CustomerEmail = "clr@gmail.com";
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();

            // Then
            result.Should().NotBeNull();
            result.FavoriteGenres.Should().NotBeEmpty();
            result.FavoriteGenres.Should().Contain(genre => genre == "Crime");
            result.FavoriteGenres.Should().Contain(genre => genre == "Thriller");
        }

        [Fact]
        public void WhenInvalidEmailIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(_context, _mapper);
            query.CustomerEmail = "test";
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found.");
        }

        [Fact]
        public void WhenNoFavoriteGenresAtCustomer__InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var customer = new Customer
            {
                Name = "Nicole",
                Surname = "Kidman",
                Email = "kidd@gmail.com",
                Password = "fidelio"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            GetMyFavoriteGenresQuery query = new GetMyFavoriteGenresQuery(_context, _mapper);
            query.CustomerEmail = "kidd@gmail.com";
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("No favorite genres found for this customer.");
        }
    }
}