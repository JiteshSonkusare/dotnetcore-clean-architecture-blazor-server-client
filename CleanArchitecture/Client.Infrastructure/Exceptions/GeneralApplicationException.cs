using System.Runtime.Serialization;

namespace Client.Infrastructure.Exceptions
{
    /// <summary>
    /// This exception class has been prepared to get rid of Sonar errors
    /// which were pointing that it is not good practice to throw
    /// ApplicationException directly.
    /// Actually, list of good practices, what to throw, what to not throw
    /// You can find on the website below:
    /// https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/using-standard-exception-types
    /// </summary>
    [Serializable]
    public class GeneralApplicationException : ApplicationException
    {
        /// <summary>
        /// General argumentless constructor.
        /// </summary>
        public GeneralApplicationException() : base() { }

        /// <summary>
        /// Constructor which allows user to pass exception message.
        /// </summary>
        /// <param name="message"></param>
        public GeneralApplicationException(string message) : base(message) { }

        /// <summary>
        /// Exception serialization constructor.
        /// </summary>
        /// <param name="serialiizationInfo"></param>
        /// <param name="context"></param>
        public GeneralApplicationException(SerializationInfo serialiizationInfo, StreamingContext context) : base(serialiizationInfo, context) { }

        /// <summary>
        /// Exception which allows to attach to itself another, inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public GeneralApplicationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
