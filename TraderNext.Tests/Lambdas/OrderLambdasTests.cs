using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using TraderNext.Api;
using TraderNext.Core.Orders.Create;
using TraderNext.Data.Mapping;
using TraderNext.Lambdas.Orders;
using Xunit;

namespace TraderNext.Tests.Lambdas
{
    public class OrderLambdasTests
    {
        [Fact]
        public void OnCreateOrder_ShouldReturnOrderWithHttpStatusCode201()
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<CommonMappingProfile>();
                config.AddProfile<OrderProfile>();
            });

            var mapper = new Mapper(configuration);

            var order = fixture.Freeze<Order>();
            var orderModel = mapper.Map<Core.Models.Order>(order);
            var createOrderRequest = fixture.Freeze<CreateOrderRequest>();
            var body = JsonConvert.SerializeObject(createOrderRequest);

            var createOrderService = fixture.Freeze<Mock<ICreateOrderService>>();
            createOrderService.Setup(m => m.CreateOrderAsync(It.IsAny<Core.Models.Order>()))
                .ReturnsAsync(orderModel);

            var services = new ServiceCollection()
                .AddTransient(s => createOrderService.Object)
                .AddTransient<IMapper>(s => mapper);
            fixture.Inject(services);

            var apiProxyRequest = fixture.Build<APIGatewayProxyRequest>()
                .With(o => o.HttpMethod, HttpMethod.Post.ToString())
                .With(o => o.Body, body)
                .Create();

            var context = fixture.Freeze<Mock<ILambdaContext>>();
            context.SetupGet(m => m.Logger).Returns(fixture.Freeze<ILambdaLogger>());

            // This is important! Otherwise it calls the no-argument constructor!
            fixture.Customize<OrderLambdas>(c => c.FromFactory(() => new OrderLambdas(services)));

            var underTest = fixture.Freeze<OrderLambdas>();

            // Act
            var actual = underTest.CreateOrder(apiProxyRequest, context.Object);

            // Assert
            actual.StatusCode.ShouldBe((int)HttpStatusCode.Created);

            actual.Body.ShouldNotBeNullOrEmpty();

            var actualOrder = JsonConvert.DeserializeObject<Order>(actual.Body);

            actualOrder.OrderId.ShouldBe(order.OrderId);
        }

        [Theory]
        [MemberData(nameof(GetExceptions))]
        public void OnCreateOrder_Fail_ShouldReturnWithHttpStatusCode<T>(T exception, HttpStatusCode httpStatusCode)
            where T: Exception
        {
            // Arrange
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<CommonMappingProfile>();
                config.AddProfile<OrderProfile>();
            });

            var mapper = new Mapper(configuration);

            var createOrderRequest = fixture.Freeze<CreateOrderRequest>();
            var body = JsonConvert.SerializeObject(createOrderRequest);

            var createOrderService = fixture.Freeze<Mock<ICreateOrderService>>();
            createOrderService.Setup(m => m.CreateOrderAsync(It.IsAny< Core.Models.Order> ()))
                .ThrowsAsync(exception);

            var services = new ServiceCollection()
                .AddTransient(s => createOrderService.Object)
                .AddTransient<IMapper>(s => mapper);
            fixture.Inject(services);

            var apiProxyRequest = fixture.Build<APIGatewayProxyRequest>()
                .With(o => o.HttpMethod, HttpMethod.Post.ToString())
                .With(o => o.Body, body)
                .Create();

            var context = fixture.Freeze<Mock<ILambdaContext>>();
            context.SetupGet(m => m.Logger).Returns(fixture.Freeze<ILambdaLogger>());

            // This is important! Otherwise it calls the no-argument constructor!
            fixture.Customize<OrderLambdas>(c => c.FromFactory(() => new OrderLambdas(services)));

            var underTest = fixture.Freeze<OrderLambdas>();

            // Act
            var actual = underTest.CreateOrder(apiProxyRequest, context.Object);

            // Assert
            actual.StatusCode.ShouldBe((int)httpStatusCode);
        }

        public static IEnumerable<object[]> GetExceptions()
        {
            var fixture = new Fixture();

            yield return new object[] { fixture.Freeze<FluentValidation.ValidationException>(), HttpStatusCode.BadRequest };
            yield return new object[] { fixture.Freeze<Exception>(), HttpStatusCode.InternalServerError };
        }
    }
}
