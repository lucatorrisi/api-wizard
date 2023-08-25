namespace APIWizard.Models.Abstracts
{
    /// <summary>
    /// Represents the base class for API wizard schema management.
    /// </summary>
    internal abstract class WizardSchemaBase
    {
        /// <summary>
        /// Gets the URI for a specific route.
        /// </summary>
        /// <param name="route">The route to generate the URI for.</param>
        /// <returns>The generated URI.</returns>
        internal abstract Uri GetUri(string route);

        /// <summary>
        /// Gets the URI for a specific route on a given server.
        /// </summary>
        /// <param name="server">The server name or address.</param>
        /// <param name="route">The route to generate the URI for.</param>
        /// <returns>The generated URI.</returns>
        internal abstract Uri GetUri(string server, string route);
    }
}
