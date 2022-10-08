using System.Net.Mime;
using WebApp.Exceptions;
using WebApp.Models.Authentication;
using WebApp.Infrastructure.Authentication.Security;
using WebApp.Infrastructure.Authentication.Security.Interfaces;

namespace WebApp.Infrastructure.Authentication.Client
{
    public abstract class ApiClientBase : IDisposable
    {
        private readonly HttpClient _client;
        private readonly IAuthHandler? _authHandler;
        private readonly IApiClientConfig _config;

        protected ApiClientBase(IApiClientConfig config, IAuthHandler? authHandler)
        {
            _authHandler = authHandler;
            _config = config;
            _client = new HttpClient() { BaseAddress = new Uri(_config.BaseUrl), Timeout = config?.Timeout ?? new TimeSpan(0, 0, 30) };
            AddDefaultHeaders(new string[] { MediaTypeNames.Application.Json });
        }

        /// <summary>
        /// Helper method to execute HTTP Request
        /// </summary>
        /// <param name="request">HTTP request object.</param>
        /// <param name="cancellation">Cancellation token to early cancel processing.</param>
        /// <returns></returns>
        private async Task<ResponseData> ExecuteRequest(HttpRequestMessage request, CancellationToken cancellation)
        {
            try
            {
                if (_authHandler != null)
                    _client.DefaultRequestHeaders.Authorization = await _authHandler.GetAuthTokenAsync(cancellation).Result.GetAuthorizationHeaderasync();
                HttpResponseMessage? response = null;
                Exception? taskError = null;
                await _client.SendAsync(request, cancellation).ContinueWith(task =>
                {
                    if (task.Exception != null)
                        taskError = task.Exception;
                    else
                        response = task.Result;
                }, cancellation);
                if (taskError != null)
                    throw taskError;
                if (response == null)
                    throw new GeneralApplicationException("Unknown error! Could not retrieve response.");

                string content = string.Empty;
                await response.Content.ReadAsStringAsync(cancellation).ContinueWith(task =>
                {
                    if (task.Exception != null)
                        taskError = task.Exception;
                    else
                        content = task.Result;
                }, cancellation);
                if (taskError != null)
                    throw taskError;

                var responseHeaders = new List<HeaderData>();
                responseHeaders.AddRange(response.Headers.Select(H => new HeaderData(true, H.Key, H.Value.ToArray())));
                return new ResponseData(response.StatusCode, content, responseHeaders.ToArray());
            }
            catch (Exception ex)
            {
                throw new GeneralApplicationException("Error occurred while executing HTTP request.", ex);
            }
        }

        /// <summary>
        /// Adds HTTP headers to by default send with every HTTP request. It is recommended to call this method in constructor only.
        /// </summary>
        /// <param name="acceptedMediaTypes">Media types accepted. Recommended to use constants defined in <see cref="System.Net.Mime.MediaTypeNames"/>.</param>
        /// <param name="headers">Default headers.</param>
        protected void AddDefaultHeaders(string[] acceptedMediaTypes, params HeaderData[] headers)
        {
            foreach (string mediaType in acceptedMediaTypes)
                _client.DefaultRequestHeaders.Accept.ParseAdd(mediaType);
            foreach (HeaderData header in headers)
                _client.DefaultRequestHeaders.Add(header.Name, header.Values);
        }

        /// <summary>
        /// Creates and sends an HTTP request to API.
        /// </summary>
        /// <param name="requestUri">API URL to send request to. This can be an absolute URL if BaseUrl is not set in <see cref="IApiClientConfig"/>.</param>
        /// <param name="method">HTTP method to be used while sending request.</param>
        /// <param name="cancellation">Token to early cancellation if needed.</param>
        /// <param name="responseParser">Action triggered when response is received from API. Should be used to parse the received response.</param>
        /// <param name="requestHeaders">Addtional request specific headers to send along with default request header.</param>
        protected async Task Send(Uri requestUri, HttpMethod method, CancellationToken cancellation, Action<ResponseData> responseParser, params HeaderData[] requestHeaders)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, requestUri);
            foreach (var header in requestHeaders)
                message.Headers.Add(header.Name, header.Values);
            var result = await ExecuteRequest(message, cancellation);
            responseParser.Invoke(result);
        }

		/// <summary>
		/// Creates and sends an HTTP request WITH BODY to API.
		/// </summary>
		/// <param name="requestUri">API URL to send request to. This can be an absolute URL if BaseUrl is not set in <see cref="IApiClientConfig"/>.</param>
		/// <param name="method">HTTP method to be used while sending request.</param>
		/// <param name="content">HTTP body content to send as part of request.</param>
		/// <param name="cancellation">Token to early cancellation if needed.</param>
		/// <param name="responseParser">Action triggered when response is received from API. Should be used to parse the received response.</param>
		/// <param name="requestHeaders">Addtional request specific headers to send along with default request header.</param>
		protected async Task Send(Uri requestUri, HttpMethod method, HttpContent content, CancellationToken cancellation, Action<ResponseData> responseParser, params HeaderData[] requestHeaders)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, requestUri);
            if (content != null)
                message.Content = content;
            if (requestHeaders != null)
                foreach (var header in requestHeaders)
                    message.Headers.Add(header.Name, header.Values);
            var result = await ExecuteRequest(message, cancellation);
            responseParser.Invoke(result);
        }

        void IDisposable.Dispose()
        {
            _client.Dispose();
        }
    }
}
