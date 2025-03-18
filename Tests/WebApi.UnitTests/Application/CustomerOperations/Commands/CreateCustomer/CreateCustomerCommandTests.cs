using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            // Given
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "Tony",
                Surname = "Montana",
                Email = "scr@gmail.com",
                Password = "eyesneverlie"
            };
            command.Model = model;
            // When
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            var result = _context.Customers.SingleOrDefault(x=> x.Email == model.Email);

            result.Should().NotBeNull();
            result.Name.Should().Be(model.Name);
            result.Surname.Should().Be(model.Surname);
            result.Password.Should().Be(model.Password);

        }

        [Fact]
        public void WhenAlreadyExistsCustomerIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            CreateCustomerModel model = new CreateCustomerModel()
            {
                Name = "Clarice",
                Surname = "Starling",
                Email = "clr@gmail.com",
                Password = "123456"
            };
            command.Model = model;
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("A user with this email already exists.");
        }
    }
}