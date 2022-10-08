using System.Net;
using Newtonsoft.Json;
using WebApp.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Infrastructure.Authentication.Client
{
    /// <summary>
    /// Response model for API Client.
    /// This is immutable object.
    /// </summary>
    /// <typeparam name="T">Model for succcesful response.</typeparam>
    public class Response<T> : IDataOutput
    {
        /// <summary>
        /// Parsed response data in case of success. Otherwise null.
        /// </summary>
        public T Data { get; private set; }
        /// <summary>
        /// Validation errors in sent request.
        /// </summary>
        public ValidationProblemDetails ValidationErrors { get; private set; }
        /// <summary>
        /// Errors encountered during request processing.
        /// </summary>
        public ProblemDetails ServerError { get; private set; }
        /// <summary>
        /// HTTP Status code for response.
        /// </summary>
        public int Status { get; private set; }
        /// <summary>
        /// Response headers.
        /// </summary>
        public IEnumerable<HeaderData> ResponseHeaders { get; private set; }
        /// <summary>
        /// Indicates if response contains error.
        /// </summary>
        public bool ContainsError { get; private set; }
        /// <summary>
        /// Statuses considered (and inputed) by class user as successful for the given API endpoint.
        /// </summary>
        private readonly int[] SuccesfulStatuses;

        /// <summary>
        /// Get the FINAL data output.
        /// </summary>
        /// <returns>
        /// If Response Status is one of status codes which are considered as sucessful (<cref>SuccesfulStatuses</cref>)
        /// function returns this.Data object, if it is not considered as successful status code, then function returns 
        /// this.ValidationErrors if it is set. If this.ValidationErrors is not set, function returns this.ServerError object.
        /// </returns>
        public object GetOutputData()
        {
            if (SuccesfulStatuses?.Contains(Status) ?? false)
                return Data;
            else
            {
                if (ValidationErrors != null)
                    return ValidationErrors;
                else
                    return ServerError;
            }
        }

        /// <summary>
        /// Creates instance using provided response data.
        /// </summary>
        /// <param name="response">Response data</param>
        /// <param name="succesfulStatuses">Array of successful HTTP status, which indicates that response content can be parsed to type <typeparamref name="T" />.</param>
        public Response(ResponseData response, params int[] succesfulStatuses)
        {
            SuccesfulStatuses = succesfulStatuses;
            Status = response.StatusCode;
            ResponseHeaders = response.ResponseHeaders;

            if (succesfulStatuses?.Contains(response.StatusCode) ?? false)
            {
                Data = response.ConvertContent<T>();
            }
            else
            {
                try
                {
                    try
                    {
                        ValidationErrors = response.ConvertContent<ValidationProblemDetails>(new JsonSerializerSettings
                        {
                            // we want exception when response from server doesn't match with ValidationProblemDetails model
                            MissingMemberHandling = MissingMemberHandling.Error
                        });
                        // Because ValidationProblemDetails is subclass of ProblemDetails, deserialization is successful
                        // even if response contains in fact ProblemDetails object (not ValidationProblemDetails), so even
                        // if Errors JSON object is not present. That's why, if Errors JSON object is empty, we are treating
                        // response as a ServerError response (not ValidationError).
                        if (ValidationErrors.Errors.Count == 0)
                        {
                            ValidationErrors = null;
                            ServerError = response.ConvertContent<ProblemDetails>();
                        }
                    }
                    catch (Exception)
                    {
                        ServerError = response.ConvertContent<ProblemDetails>();
                    }
                    ContainsError = true;
                }
                catch
                {
                    throw new GeneralApplicationException($"Could not parse response for status: {response.StatusCode}, data: {response.Content}.");
                }
            }
        }

        /// <summary>
        /// Creates instance using provided response data.
        /// </summary>
        /// <param name="response">Response data</param>
        /// <param name="succesfulStatuses">Array of successful HttpStatusCode(s), which indicates sucessful response.</param>
        public Response(ResponseData response, params HttpStatusCode[] succesfulStatuses) : this(response, Array.ConvertAll(succesfulStatuses, item => (int)item))
        { }
    }
}
