using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _configuration = testFixture.Configuration;
        }

        [Fact]
        public void WhenValidTokenIsGiven_Token_ShouldBeRefreshed()
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.refreshToken = RefreshToken(); 
            // When
            var result = FluentActions.Invoking(()=> command.Handle()).Invoke();
            // Then
            result.AccessToken.Should().NotBeNull();
            result.RefreshToken.Should().NotBeNull();
            result.Expiration.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void WhenInvalidTokenIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.refreshToken = "test";
            
            // When && Then
            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("No valid refresh token found");
        }

        private string RefreshToken()
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            CreateTokenModel model = new CreateTokenModel()
            {
                Email = "clr@gmail.com",
                Password = "123456"
            };
            command.Model = model;

            var result = FluentActions.Invoking(()=> command.Handle()).Invoke();

            return result.RefreshToken;
        }
    }
}