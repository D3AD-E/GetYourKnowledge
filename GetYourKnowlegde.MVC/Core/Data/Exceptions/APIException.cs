using System;

namespace GetYourKnowledge.MVC.Core.Data.Exceptions
{
    /// <summary>
    /// Exception thrown then an API fails
    /// </summary>
    public class APIException : Exception
    {
        public int StatusCode { get; private set; }

        public APIException()
        {
            StatusCode = 403;
        }

        public APIException(string message)
        : base(message)
        {
            StatusCode = 403;
        }

        public APIException(string message, Exception inner)
            : base(message, inner)
        {
            StatusCode = 403;
        }
    }
}
