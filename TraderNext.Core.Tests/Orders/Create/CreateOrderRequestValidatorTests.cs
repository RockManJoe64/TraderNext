using System.Collections.Generic;
using AutoFixture;
using Shouldly;
using TraderNext.Models;
using Xunit;

namespace TraderNext.Orders.Create
{
    public class CreateOrderRequestValidatorTests
    {
        private static readonly Fixture _fixture = new Fixture();

        [Fact]
        public void ShouldPassValidation()
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                Order = _fixture.Create<Order>()
            };

            var underTest = new CreateOrderRequestValidator();

            // Act
            var actual = underTest.Validate(request);

            // Assert
            actual.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFailWhenOrderIsNull()
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                Order = null,
            };

            var underTest = new CreateOrderRequestValidator();

            // Act
            var actual = underTest.Validate(request);

            // Assert
            actual.IsValid.ShouldBeFalse();
        }

        [Theory]
        [MemberData(nameof(GetOrders))]
        public void ShouldFailWhenPropertyIsNull(Order order)
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                Order = order,
            };

            var underTest = new CreateOrderRequestValidator();

            // Act
            var actual = underTest.Validate(request);

            // Assert
            actual.IsValid.ShouldBeFalse();
        }

        public static IEnumerable<object[]> GetOrders()
        {
            yield return new object[] { _fixture.Build<Order>().Without(o => o.OrderId).Create() };
            yield return new object[] { _fixture.Build<Order>().Without(o => o.Price).Create() };
            yield return new object[] { _fixture.Build<Order>().Without(o => o.Quantity).Create() };
            yield return new object[] { _fixture.Build<Order>().Without(o => o.Symbol).Create() };
        }
    }
}
