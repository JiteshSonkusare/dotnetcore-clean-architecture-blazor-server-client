using System.Net;

namespace WebApp.Exceptions
{
    /// <summary>
    /// Exception used to detail out errors in API requests.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Status code for HTTP Operation. Recommended to use value constants from class <see cref="HttpStatusCode" />.
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Error code, describing details of the failure.
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Indicates that exception is critical exception; and must be logged with logging level critical.
        /// </summary>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Initializes instance of WebApiException
        /// </summary>
        /// <param name="statusCode">Status code for HTTP Operation. Recommended to use value constants from class <see cref="StatusCodes" />.</param>
        /// <param name="errorCode">Error code, describing details of the failure.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ApiException(string message, int errorCode = 9999, int statusCode = (int)HttpStatusCode.InternalServerError, Exception? inner = default) : base(message, inner)
        {
            if (statusCode < (int)HttpStatusCode.BadRequest)
                throw new NotSupportedException($"Provided statuscode value does not indicates error situation. It cannot be less that {HttpStatusCode.BadRequest}.");

            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
