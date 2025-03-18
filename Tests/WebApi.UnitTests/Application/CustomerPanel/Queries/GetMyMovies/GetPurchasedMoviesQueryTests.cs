using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.Entities;
using WepApi.Applications.CustomerPanel.Queries.GetMyMovies;
using Xunit;

namespace Tests.Application.CustomerPanel.Queries.GetMyMovies
{
    public class GetMyMoviesQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMyMoviesQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidEmailisGiven_Movies_ShouldBeReturn()
        {
            // Given
            GetMyMoviesQuery query = new GetMyMoviesQuery(_context, _mapper);
            query.CustomerEmail = "clr@gmail.com";
            // When
            var result =FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            result.Should().NotBeNull();
            result.PurchasedMovies.Should().NotBeNull().And.NotBeEmpty();
            result.PurchasedMovies.Should().Contain(movie=> movie == "The Silence of the Lambs");
        }

        [Fact]
        public void WhenInvalidEmailisGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            GetMyMoviesQuery query = new GetMyMoviesQuery(_context, _mapper);
            query.CustomerEmail = "test";
            // When && Then
            FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found.");
        }

        [Fact]
        public void WhenNoPurchasedMovieAtCustomer__InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var customer = new Customer
            {
                Name = "Eric",
                Surname = "Draven",
                Email = "brandon@gmail.com",
                Password = "thecrow"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            GetMyMoviesQuery query = new GetMyMoviesQuery(_context, _mapper);
            query.CustomerEmail = "brandon@gmail.com";
            // When && Then
            FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("No Purchased movie found for this customer.");
        }
    }
}