using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }        

        [Fact]
        public void WhenValidIdIsGiven_Customer_ShouldBeDeleted()
        {
            // Given 
            var customer = new Customer
            {
                Name = "Sweeney",
                Surname = "Todd",
                Email = "tod@gmail.com",
                Password = "thebarber",
                IsActive = true
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Customers.SingleOrDefault(x=> x.Id == customer.Id);

            result.IsActive.Should().Be(false);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Customers.Count()+99;
            
            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = invalidId;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found");
        }

        [Fact]
        public void WhenAlreadyDeletedCustomerIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given 
            var customer = new Customer
            {
                Name = "Johanna",
                Surname = "Barker",
                Email = "jhh@gmail.com",
                Password = "ifeelyou",
                IsActive = false
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
            command.CustomerId = customer.Id;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer is already inactive");
        }
    }
}