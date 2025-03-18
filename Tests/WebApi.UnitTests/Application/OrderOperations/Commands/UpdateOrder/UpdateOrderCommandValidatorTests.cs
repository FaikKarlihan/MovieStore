using System;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.OrderOperations.Commands.UpdateOrder;
using Xunit;

namespace Tests.Application.OrderOperations.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            // Given
            UpdateOrderCommand command = new UpdateOrderCommand(null);
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
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError()
        {
            // Given
            UpdateOrderCommand command = new UpdateOrderCommand(null);
            UpdateOrderModel model = new UpdateOrderModel()
            {
                Customer = "Travis Bickle",
                Movie = "The Silence of the Lambs",
                OrderDate = new DateTime (2000, 1, 1),
                TotalPrice = 1                
            };
            command.Model = model;
            command.OrderId = 0;
            // When
            UpdateOrderCommandValidator validator = new UpdateOrderCommandValidator();
            var result = validator.Validate(command);
            // Then
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}