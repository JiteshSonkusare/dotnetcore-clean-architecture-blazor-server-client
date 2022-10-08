namespace Client.Infrastructure.Client
{
    /// <summary>
    /// Represents HTTP header data.
    /// </summary>
    public class HeaderData
    {
        /// <summary>
        /// HTTP header name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// HTTP header value
        /// </summary>
        public string[] Values { get; private set; }

        /// <summary>
        /// Creates instance of headerdata.
        /// </summary>
        /// <param name="name">Name of header. Should not be null/empty.</param>
        /// <param name="values">Values for header. Items should not be null/empty.</param>
        public HeaderData(string name, params string[] values) : this(false, name, values)
        {

        }

        internal HeaderData(bool isResponseHeader, string name, params string[] values)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name for header cannot be null or empty.");
            if ((values?.Length ?? 0) == 0)
                throw new ArgumentException($"Atleast 1 value should be present for header '{name}'.");
            if (!isResponseHeader && values.All(v => string.IsNullOrWhiteSpace(v)))
                throw new ArgumentException($"value elements for header '{name}' cannot be null or empty.");
            Name = name;
            Values = values;
        }
    }
}
