using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerPanel.Commands.AddFavoriteGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.CustomerPanel.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public AddFavoriteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeAddedToFavorites()
        {
            // Given
            var customer = new Customer
            {
                Name = "Marlon",
                Surname = "Brando",
                Email = "marr@gmail.com",
                Password = "vitocorleone"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_context);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = "Drama"
            };
            command.Model = model;
            command.Email = customer.Email;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            var result = _context.Customers.SingleOrDefault(x=> x.Id == customer.Id);
            // Then
            result.FavouriteGenres.Should().Contain(x => x.Name == model.Genre);
        }

        [Fact]
        public void WhenInvalidEmailisGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_context);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = "Drama"
            };
            command.Model = model;
            command.Email = "test";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found");
        }

        [Fact]
        public void WhenInvalidGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_context);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = "test"
            };
            command.Model = model;
            command.Email = "clr@gmail.com";

            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Genre not found");
        }

        [Fact]
        public void WhenAlreadyFavoriteGenreIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_context);
            AddFavoriteGenreModel model = new AddFavoriteGenreModel()
            {
                Genre = "Crime"
            };
            command.Model = model;
            command.Email = "clr@gmail.com";
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Genre is already in favorites");
        }
    }
}