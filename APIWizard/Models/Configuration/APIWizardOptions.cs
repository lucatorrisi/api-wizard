using APIWizard.Constants;

namespace APIWizard.Models.Configuration
{
    /// <summary>
    /// Represents the options for configuring the APIWizard.
    /// </summary>
    public class APIWizardOptions
    {
        /// <summary>
        /// Gets or sets the lifetime of pooled HTTP connections.
        /// </summary>
        /// <remarks>
        /// This property defines how long an HTTP connection can remain in the connection pool before it's closed and replaced.
        /// </remarks>
        public TimeSpan PooledConnectionLifetime { get; set; } = HttpClientDefaults.PooledConnectionLifetime;

        /// <summary>
        /// Gets or sets the list of additional servers for the APIWizard.
        /// </summary>
        /// <remarks>
        /// This property allows you to specify additional servers that the APIWizard should work with, apart from the default server.
        /// </remarks>
        public List<string> AdditionalServers { get; set; } = new List<string>();
    }
}
