using System.Net;
using Newtonsoft.Json;

namespace WebApp.Infrastructure.Authentication.Client
{
    /// <summary>
    /// Represents HTTP response data.
    /// </summary>
    public sealed class ResponseData
    {
        /// <summary>
        /// HTTP response status code.
        /// </summary>
        public int StatusCode { get; private set; }
        /// <summary>
        /// String representation of response content
        /// </summary>
        public string Content { get; private set; }
        /// <summary>
        /// Response headers.
        /// </summary>
        public IEnumerable<HeaderData> ResponseHeaders { get; private set; }

        /// <summary>
        /// ResponseData object constructor.
        /// </summary>
        /// <param name="statusCode">HTTP status code of the response.</param>
        /// <param name="content">Content of the response.</param>
        /// <param name="responseHeaders">List of response HTTP headers.</param>
        public ResponseData(HttpStatusCode statusCode, string content, IEnumerable<HeaderData> responseHeaders)
        {
            StatusCode = (int)statusCode;
            Content = content;
            ResponseHeaders = responseHeaders;
        }

        /// <summary>
        /// ResponseData object constructor.
        /// </summary>
        /// <param name="statusCode">HTTP status code of the response.</param>
        /// <param name="content">Content of the response.</param>
        /// <param name="responseHeaders">Array of response HTTP headers.</param>
        public ResponseData(HttpStatusCode statusCode, string content, params HeaderData[] responseHeaders) : this(statusCode, content, responseHeaders.ToList())
        { }


        /// <summary>
        /// Converts response contents to specified object type.
        /// </summary>
        /// <typeparam name="T">Type of object to convert into.</typeparam>
        /// <returns>Object of specified type.</returns>
        public T ConvertContent<T>() => JsonConvert.DeserializeObject<T>(Content);

        /// <summary>
        /// Converts response contents to specified object type.
        /// </summary>
        /// <param name="settings">Deserialization settings. Allows to change deserialization behaviour to more strict when - for example - some field doesn't exist in the JSON response.</param>
        /// <typeparam name="T">Type of object to convert into.</typeparam>
        /// <returns>Object of specified type.</returns>
        public T ConvertContent<T>(JsonSerializerSettings settings) => JsonConvert.DeserializeObject<T>(Content, settings);

        /// <summary>
        /// Converts response contents to specified object type.
        /// </summary>
        /// <param name="objectType">Type of object to convert into.</param>
        /// <returns>Object of specified type.</returns>
        public object ConvertContent(Type objectType) => JsonConvert.DeserializeObject(Content, objectType);

        /// <summary>
        /// Converts response contents to specified object type.
        /// </summary>
        /// <param name="objectType">Type of object to convert into.</param>
        /// <param name="settings">Deserialization settings. Allows to change deserialization behaviour to more strict when - for example - some field doesn't exist in the JSON response.</param>
        /// <returns>Object of specified type.</returns>
        public object ConvertContent(Type objectType, JsonSerializerSettings settings) => JsonConvert.DeserializeObject(Content, objectType, settings);
    }
}
