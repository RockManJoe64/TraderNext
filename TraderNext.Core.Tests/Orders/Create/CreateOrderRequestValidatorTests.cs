using System.Collections.Generic;
using AutoFixture;
using Shouldly;
using TraderNext.Models;
using Xunit;

namespace TraderNext.Orders.Create
{
    public class CreateOrderRequestValidatorTests
    {
        [Fact]
        public void ShouldPassValidation()
        {
            // Arrange
            var fixture = new Fixture();

            var request = fixture.Freeze<CreateOrderRequest>();

            var underTest = fixture.Freeze<CreateOrderRequestValidator>();

            // Act
            var actual = underTest.Validate(request);

            // Assert
            actual.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ShouldFailWhenOrderIsNull()
        {
            // Arrange
            var fixture = new Fixture();

            var request = fixture.Build<CreateOrderRequest>()
                .Without(o => o.Order)
                .Create();

            var underTest = fixture.Freeze<CreateOrderRequestValidator>();

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
            var fixture = new Fixture();
            fixture.Inject(order);

            var request = fixture.Freeze<CreateOrderRequest>();

            var underTest = fixture.Freeze<CreateOrderRequestValidator>();

            // Act
            var actual = underTest.Validate(request);

            // Assert
            actual.IsValid.ShouldBeFalse();
        }

        public static IEnumerable<object[]> GetOrders()
        {
            var fixture = new Fixture();

            yield return new object[] { fixture.Build<Order>().Without(o => o.OrderId).Create() };
            yield return new object[] { fixture.Build<Order>().Without(o => o.Price).Create() };
            yield return new object[] { fixture.Build<Order>().Without(o => o.Quantity).Create() };
            yield return new object[] { fixture.Build<Order>().Without(o => o.Symbol).Create() };
        }
    }
}
