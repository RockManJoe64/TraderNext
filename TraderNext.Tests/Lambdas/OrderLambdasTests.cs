using System.Net;
using System.Net.Http;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Create;
using TraderNext.Core.Orders.Repository;
using TraderNext.Data.Relational;
using TraderNext.Lambdas.Orders;
using Xunit;

namespace TraderNext.Tests.Lambdas
{
    public class OrderLambdasTests
    {
        [Fact]
        public void OnCreateOrder_ShouldReturnOrderWithHttpStatusCode200()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var order = fixture.Freeze<Order>();
            var createOrderRequest = fixture.Freeze<CreateOrderRequest>();
            var body = JsonSerializer.Serialize(createOrderRequest);

            var dbContext = new Mock<DbContext>();
            fixture.Inject(dbContext.Object);

            var lambdaDbContext = new Mock<LambdaDbContext>();
            fixture.Inject(lambdaDbContext.Object);

            var orderRepository = fixture.Freeze<Mock<IOrderRepository>>();

            var createOrderService = fixture.Freeze<Mock<ICreateOrderService>>();
            createOrderService.Setup(m => m.CreateOrderAsync(It.IsAny<CreateOrderRequest>()))
                .ReturnsAsync(order);

            var services = new ServiceCollection()
                .AddTransient(s => dbContext.Object)
                .AddTransient(s => lambdaDbContext.Object)
                .AddTransient(s => orderRepository.Object)
                .AddTransient(s => createOrderService.Object);
            fixture.Inject(services);

            var apiProxyRequest = fixture.Build<APIGatewayProxyRequest>()
                .With(o => o.HttpMethod, HttpMethod.Post.ToString())
                .With(o => o.Body, body)
                .Create();

            var context = fixture.Freeze<Mock<ILambdaContext>>();
            context.SetupGet(m => m.Logger).Returns(fixture.Freeze<ILambdaLogger>());

            fixture.Customize<OrderLambdas>(c => c.FromFactory(() => new OrderLambdas(services)));

            var underTest = fixture.Freeze<OrderLambdas>();

            // Act
            var actual = underTest.CreateOrder(apiProxyRequest, context.Object);

            // Assert
            actual.StatusCode.ShouldBe((int)HttpStatusCode.Created);
        }
    }
}
