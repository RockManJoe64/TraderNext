using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Shouldly;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Fixtures;
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

            var request = fixture.Build<Order>()
                .Without(o => o.ID)
                .Create();

            var expectedAmount = request.Price * request.Quantity;

            var orderTypeRepository = fixture.Freeze<Mock<IOrderTypeRepository>>();

            orderTypeRepository.Setup(m => m.EnrichOrderTypeFieldAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order o) => o);

            var orderRepository = fixture.Freeze<Mock<IOrderRepository>>();

            orderRepository.Setup(m => m.CreateOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order o) => o);

            var underTest = fixture.Freeze<CreateOrderService>();

            // Act
            var actualResult = await underTest.CreateOrderAsync(request);

            // Assert
            orderRepository.Verify(m => m.CreateOrderAsync(It.IsAny<Order>()), 
                Times.AtLeastOnce());

            orderTypeRepository.Verify(m => m.EnrichOrderTypeFieldAsync(It.IsAny<Order>()), 
                Times.AtLeastOnce());

            actualResult.Amount.ShouldBe(expectedAmount);
        }
    }
}
