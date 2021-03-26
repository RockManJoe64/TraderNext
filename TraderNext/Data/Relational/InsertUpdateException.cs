using System;
using System.Net;
using TraderNext.Common.Exceptions;

namespace TraderNext.Data.Relational
{
    public class InsertUpdateException : HttpStatusException
    {
        public InsertUpdateException(string message)
            : base(HttpStatusCode.InternalServerError, message)
        {
        }

        public InsertUpdateException(string message, Exception innerException)
            : base(HttpStatusCode.InternalServerError, message, innerException)
        {
        }
    }
}
