using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerPanel.Commands.BuyMovie;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.CustomerPanel.Commands.BuyMovie
{
    public class BuyMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public BuyMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBePurchased_AndOrderCreated()
        {
            // Given
            BuyMovieCommand command = new BuyMovieCommand(_context);
            BuyMovieModel model = new BuyMovieModel()
            {
                Movie = "Fracture"
            };
            command.Model = model;
            command.Email = "clr@gmail.com";
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Customers.SingleOrDefault(x=> x.Email == "clr@gmail.com");

            Assert.Contains(result.PurchasedMovies, movie => movie.Name == model.Movie);

            var order = _context.Orders.SingleOrDefault(x=> x.Movie.Name == "Fracture");
            order.CustomerId.Should().Be(1);
            order.TotalPrice.Should().Be(15.99);
            order.Id.Should().Be(2);
        }

        [Fact]
        public void WhenInvalidEmailisGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            BuyMovieCommand command = new BuyMovieCommand(_context);
            BuyMovieModel model = new BuyMovieModel()
            {
                Movie = "Fracture"
            };
            command.Model = model;
            command.Email = "test";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found");
        }

        [Fact]
        public void WhenInvalidMovieIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            BuyMovieCommand command = new BuyMovieCommand(_context);
            BuyMovieModel model = new BuyMovieModel()
            {
                Movie = "test"
            };
            command.Model = model;
            command.Email = "clr@gmail.com";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Movie not found");
        }
    }
}