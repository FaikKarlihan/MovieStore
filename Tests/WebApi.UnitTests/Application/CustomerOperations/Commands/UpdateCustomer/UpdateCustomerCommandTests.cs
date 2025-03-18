using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.UpdateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeUpdated()
        {
            // Given
            var customer = new Customer
            {
                Name = "Johnny",
                Surname = "Depp",
                Email = "jhn@gmail.com",
                Password = "captain"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            UpdateCustomerCommand command = new UpdateCustomerCommand(_context);
            UpdateCustomerModel model = new UpdateCustomerModel()
            {
                Name = "Jack",
                Surname = "Sparrow",
                Email = "jck@gmail.com",
                Password = "blackpearl",
                IsActive = true
            };
            command.Model = model;
            command.CustomerId = customer.Id;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Customers.SingleOrDefault(x=> x.Id == customer.Id);

            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
            result.Email.Should().Be(model.Email);
            result.Password.Should().Be(model.Password);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            int invalidId = _context.Customers.Count()+99;

            UpdateCustomerCommand command = new UpdateCustomerCommand(_context);
            UpdateCustomerModel model = new UpdateCustomerModel()
            {
                Name = "test",
                Surname = "test",
                Email = "test@gmail.com",
                Password = "testtest",
                IsActive = true
            };
            command.Model = model;
            command.CustomerId = invalidId;
            
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Customer not found");
        }
    }
}