using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using TraderNext.Common.Exceptions;
using TraderNext.Core.Orders.Create;
using TraderNext.Core.Orders.Fetch;
using TraderNext.Core.Orders.Repository;
using TraderNext.Data.Relational.Extensions;
using TraderNext.Data.Relational.Repositories;

namespace TraderNext.Lambdas.Orders
{
    public class OrderLambdas
    {
        private IServiceCollection _services;

        public OrderLambdas()
        {
            ConfigureServices();
        }

        // For unit testing only
        public OrderLambdas(IServiceCollection services)
        {
            _services = services;
        }

        private void ConfigureServices()
        {
            _services = new ServiceCollection();
            _services
                .AddMySqlDbContext()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IFetchOrdersService, FetchOrdersService>()
                .AddTransient<ICreateOrderService, CreateOrderService>()
                .AddScoped<CreateOrderRequestValidator>();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public APIGatewayProxyResponse GetOrders(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("GetOrders Request\n");

            var fetchOrdersRequest = JsonSerializer.Deserialize<FetchOrdersRequest>(request.Body);

            using ServiceProvider serviceProvider = _services.BuildServiceProvider();
            var fetchOrderService = serviceProvider.GetService<IFetchOrdersService>();
            var orders = fetchOrderService.FetchOrdersAsync(fetchOrdersRequest)
                .GetAwaiter()
                .GetResult();

            var body = JsonSerializer.Serialize(orders);
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = body,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };

            return response;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public APIGatewayProxyResponse CreateOrder(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("CreateOrder Request\n");

            var createOrderRequest = JsonSerializer.Deserialize<CreateOrderRequest>(request.Body);

            using ServiceProvider serviceProvider = _services.BuildServiceProvider();
            var createOrderService = serviceProvider.GetService<ICreateOrderService>();
            try
            {
                var order = createOrderService.CreateOrderAsync(createOrderRequest)
                    .GetAwaiter()
                    .GetResult();

                var body = JsonSerializer.Serialize(order);
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Body = body,
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };

                return response;
            }
            catch (FluentValidation.ValidationException exception)
            {
                return exception.CreateResponse();
            }
            catch (Exception exception)
            {
                return exception.CreateResponse();
            }
        }
    }
}
