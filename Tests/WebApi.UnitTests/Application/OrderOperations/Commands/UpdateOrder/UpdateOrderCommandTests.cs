using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Commands.UpdateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Order_ShouldBeUpdated()
        {
            // Given
            var customer = new Customer
            {
                Name = "Travis",
                Surname = "Bickle",
                Email = "trvs@gmail.com",
                Password = "youTalkinToMe"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            UpdateOrderModel model = new UpdateOrderModel()
            {
                Customer = "Travis Bickle",
                Movie = "The Silence of the Lambs",
                OrderDate = new DateTime (2000, 1, 1),
                TotalPrice = 1
            };
            command.Model = model;
            command.OrderId = 1;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var order = _context.Orders.SingleOrDefault(x=> x.Id == 1);

            order.Should().NotBeNull();
            order.Customer.Name.Should().Be("Travis");
            order.TotalPrice.Should().Be(1);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Orders.Count()+99;

            UpdateOrderCommand command = new UpdateOrderCommand(_context);
            UpdateOrderModel model = new UpdateOrderModel()
            {
                Customer = "Travis Bickle",
                Movie = "The Silence of the Lambs",
                OrderDate = new DateTime (2000, 1, 1),
                TotalPrice = 1
            };
            command.Model = model;
            command.OrderId = invalidId;
            
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Order not found");
        }
    }
}