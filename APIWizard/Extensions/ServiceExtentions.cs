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
        public static void AddAPIWizard(this IServiceCollection services, IAPIClient apiClient)
        {
            services.AddSingleton(apiClient);
        }
    }
}
