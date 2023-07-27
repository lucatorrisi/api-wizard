using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Extensions
{
    public static class ServiceExtentions
    {
        /// <summary>
        /// Adds the APIWizard client implementation as a singleton service to the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection instance to add the service to.</param>
        /// <param name="apiClient">The instance of the API client to be registered as a singleton.</param>
        public static void AddAPIWizardClient(this IServiceCollection services, IAPIClient apiClient)
        {
            services.AddSingleton(apiClient);
        }
    }
}
