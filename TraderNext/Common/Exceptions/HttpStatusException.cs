using System;
using System.Collections.Generic;
using System.Net;

namespace TraderNext.Common.Exceptions
{
    public abstract class HttpStatusException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }

        public HttpStatusException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusException(HttpStatusCode httpStatusCode, string message) 
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusException(HttpStatusCode httpStatusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
