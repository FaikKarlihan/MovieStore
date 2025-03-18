using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Queries.GetOrderDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.OrderOperations.Query
{
    public class GetOrderDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Order_ShouldBeReturn()
        {
            // Given
            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = 1;
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            result.Should().NotBeNull();
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Orders.Count()+99;

            GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
            query.OrderId = invalidId;
            // When
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Order not found");
        }
    }
}