namespace APIWizard.Models.Abstracts
{
    /// <summary>
    /// Represents the base class for details of an API path.
    /// </summary>
    internal abstract class PathDetailBase
    {
        /// <summary>
        /// Gets the content type associated with the path detail.
        /// </summary>
        /// <returns>The content type.</returns>
        internal abstract string? GetContentType();

        /// <summary>
        /// Checks if the path detail has a body parameter.
        /// </summary>
        /// <returns><c>true</c> if a body parameter is present; otherwise, <c>false</c>.</returns>
        internal abstract bool HasBodyParameter();

        /// <summary>
        /// Checks if a body parameter is required for the path detail.
        /// </summary>
        /// <returns><c>true</c> if a body parameter is required; otherwise, <c>false</c>.</returns>
        internal abstract bool IsBodyRequired();

        /// <summary>
        /// Adds a dummy body parameter to the path detail.
        /// </summary>
        internal abstract void AddDummyBodyParam();
    }
}
