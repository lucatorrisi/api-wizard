using APIWizard.Constants;
using APIWizard.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace APIWizard.Extensions
{
    /// <summary>
    /// Extension methods for configuring API Wizard client in IServiceCollection.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the API Wizard client to the IServiceCollection as a singleton.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <param name="apiClient">The instance of the API client to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="apiClient"/> is null.</exception>
        public static void AddAPIWizardClient(this IServiceCollection services, IAPIClient apiClient)
        {
            ValidationUtils.ArgumentNotNull(services, nameof(services), ExceptionMessages.ServiceCollectionNull);
            ValidationUtils.ArgumentNotNull(apiClient, nameof(apiClient), ExceptionMessages.APIClientNull);
            
            services.AddSingleton(apiClient);
        }
    }
}
