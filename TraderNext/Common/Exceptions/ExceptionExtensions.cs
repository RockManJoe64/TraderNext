using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;

namespace TraderNext.Common.Exceptions
{
    public static class ExceptionExtensions
    {
        public static APIGatewayProxyResponse CreateResponse(this Exception exception)
        {
            return CreateApiGatewayResponse(HttpStatusCode.InternalServerError, exception.InnerException, exception.Message);
        }

        public static APIGatewayProxyResponse CreateResponse(this FluentValidation.ValidationException exception)
        {
            return CreateApiGatewayResponse(HttpStatusCode.BadRequest, exception.InnerException, exception.Message);
        }

        public static APIGatewayProxyResponse CreateResponse(this HttpStatusException exception)
        {
            return CreateApiGatewayResponse(exception.HttpStatusCode, exception.InnerException, exception.Message);
        }

        private static APIGatewayProxyResponse CreateApiGatewayResponse(
            HttpStatusCode httpStatus,
            Exception innerException,
            string message)
        {
            var errorMessage = message;

            if (innerException != null)
            {
                errorMessage += Environment.NewLine + innerException.StackTrace;
            }

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)httpStatus,
                Body = errorMessage,
                Headers = new Dictionary<string, string> { { "Content-Type", "plain/text" } }
            };
        }
    }
}
