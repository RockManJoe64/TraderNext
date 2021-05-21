using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TraderNext.Api;
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
                .AddAutoMapper(typeof(OrderLambdas))
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IFetchOrdersService, FetchOrdersService>()
                .AddTransient<ICreateOrderService, CreateOrderService>()
                .AddScoped<CreateOrderRequestValidator>();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public APIGatewayProxyResponse GetOrders(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("GetOrders Request\n");

            // TODO this would be used for advanced search
            //var fetchOrdersRequest = FetchOrdersRequest.FromJson(request.Body);

            using ServiceProvider serviceProvider = _services.BuildServiceProvider();
            var fetchOrderService = serviceProvider.GetService<IFetchOrdersService>();
            var mapper = serviceProvider.GetService<IMapper>();

            var orders = fetchOrderService.FetchOrdersAsync()
                .GetAwaiter()
                .GetResult();

            var responseOrders = mapper.Map<IEnumerable<Core.Models.Order>, IEnumerable<Order>>(orders);

            var body = JsonConvert.SerializeObject(orders);
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

            var createOrderRequest = CreateOrderRequest.FromJson(request.Body);

            using ServiceProvider serviceProvider = _services.BuildServiceProvider();
            var createOrderService = serviceProvider.GetService<ICreateOrderService>();
            var mapper = serviceProvider.GetService<IMapper>();

            try
            {
                var order = mapper.Map<Core.Models.Order>(createOrderRequest.Order);

                var createdOrder = createOrderService.CreateOrderAsync(order)
                    .GetAwaiter()
                    .GetResult();

                var responseOrder = mapper.Map<Order>(createdOrder);

                var body = responseOrder.ToJson();
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
