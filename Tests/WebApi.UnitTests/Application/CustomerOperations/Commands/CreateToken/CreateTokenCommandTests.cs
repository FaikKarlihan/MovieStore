using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public CreateTokenCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _configuration = testFixture.Configuration;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Token_ShouldBeCreated()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "clr@gmail.com",
                Password = "123456"
            };
            command.Model = model;
            // When
            var result = FluentActions.Invoking(()=> command.Handle()).Invoke();
            
            // Then
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().NotBeNull();
            result.Expiration.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "test",
                Password = "test"
            };
            command.Model = model;

            // When && Then
            var result = FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Username - Password is incorrect");
        }
    }
}