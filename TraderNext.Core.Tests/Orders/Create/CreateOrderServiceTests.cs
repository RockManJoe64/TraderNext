using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Shouldly;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Repository;
using Xunit;

namespace TraderNext.Core.Orders.Create
{
    public class CreateOrderServiceTests
    {
        [Fact]
        public async void ShouldCreateOrder()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var originalOrder = fixture.Build<Order>()
                .Without(o => o.ID)
                .Create();
            fixture.Inject(originalOrder);

            var request = fixture.Freeze<Order>();

            var orderRepository = fixture.Freeze<Mock<IOrderRepository>>();
            orderRepository.Setup(m => m.CreateOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(() => 
                {
                    var order = fixture.Create<Order>();
                    order.ID = fixture.Create<long>();
                    return order;
                })
                .Verifiable();

            var validator = fixture.Freeze<Mock<OrderValidator>>();

            var underTest = fixture.Freeze<CreateOrderService>();

            // Act
            var actualResult = await underTest.CreateOrderAsync(request);

            // Assert
            orderRepository.Verify();

            actualResult.ShouldBeEquivalentTo(originalOrder);
        }
    }
}
