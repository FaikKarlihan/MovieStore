using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.CustomerOperations.Query
{
    public class GetCustomerDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetCustomerDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Customer_ShouldBeReturn()
        {
            // Given
            var customer = new Customer
            {
                Name = "Tyler",
                Surname = "Durden",
                Email = "fght@gmail.com",
                Password = "nottalkaboutfc"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = customer.Id;
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();

            // Then
            result.Should().NotBeNull();
            result.FullName.Should().Be(customer.Name+" "+customer.Surname);
            result.Email.Should().Be(customer.Email);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Customers.Count()+99;

            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = invalidId;
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found");
        }
    }
}