namespace Client.Infrastructure.Client
{
    /// <summary>
    /// This interface allows to standardize function used to get FINAL data 
    /// output which will be returned in HTTP response.
    /// It has been introduced to limit mess in case of successfull/fail API 
    /// responses and different data sources (Data if HTTP/200, Server error if not HTTP/200)
    /// </summary>
    public interface IDataOutput
    {
        /// <summary>
        /// MUST return FINAL response of the API endpoint.
        /// </summary>
        /// <returns>MUST return FINAL response of the API endpoint.</returns>
        object GetOutputData();
    }
}
