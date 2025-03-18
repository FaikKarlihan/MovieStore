using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteOrderCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidIdIsGiven_Order_ShouldBeDeleted()
        {
            // Given
            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = 1;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var order = _context.Orders.SingleOrDefault(x=> x.Id == 1);

            order.Should().BeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Orders.Count()+99;

            DeleteOrderCommand command = new DeleteOrderCommand(_context);
            command.OrderId = invalidId;
            // When
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Order not found");
        }
    }
}