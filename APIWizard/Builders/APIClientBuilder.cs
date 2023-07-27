using APIWizard.Constants;
using APIWizard.Extensions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using APIWizard.Models;

namespace APIWizard.Builders
{
    public class APIClientBuilder
    {
        private WizardSchema? schema;

        /// <summary>
        /// Sets the configuration of the APIClient using a configuration file path.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        /// <returns>The APIClientBuilder instance.</returns>
        public APIClientBuilder WithConfigurationFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Invalid file path.", nameof(path));
            }

            string fullPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location ?? string.Empty) ?? string.Empty, path);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("Configuration file not found.", path);
            }

            string json = File.ReadAllText(fullPath);
            schema = JsonConvert.DeserializeObject<WizardSchema>(json) ?? throw new ArgumentException("Invalid configuration file format.", path);
            return this;
        }
        /// <summary>
        /// Sets the configuration of the APIClient using an IConfigurationSection object.
        /// </summary>
        /// <param name="section">The IConfigurationSection object containing the configuration.</param>
        /// <returns>The APIClientBuilder instance.</returns>
        public APIClientBuilder WithConfiguration(IConfigurationSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            schema = section.Get<WizardSchema>(options =>
            {
                options.BindNonPublicProperties = true;
            });
            return this;
        }
        /// <summary>
        /// Sets the configuration of the APIClient using a Swagger URL to fetch the configuration JSON.
        /// </summary>
        /// <param name="jsonSwaggerUrl">The URL pointing to the Swagger JSON configuration.</param>
        /// <returns>The APIClientBuilder instance.</returns>
        public APIClientBuilder WithSwaggerUrlConfiguration(string jsonSwaggerUrl)
        {
            using var httpClient = new HttpClient();
            string responseBody = httpClient.GetStringAsync(jsonSwaggerUrl).Result;
            schema = JsonConvert.DeserializeObject<WizardSchema>(responseBody) ?? throw new ArgumentException("Invalid configuration file format.", jsonSwaggerUrl);
            return this;
        }
        /// <summary>
        /// Builds and returns an instance of the APIClient.
        /// </summary>
        /// <returns>An instance of the APIClient.</returns>
        public IAPIClient Build()
        {
            if (schema == null)
            {
                throw new InvalidOperationException("Configuration is not set. Please use 'Configuration' methods.");
            }

            return new APIClient(HttpClientDefaults.PooledConnectionLifetime, schema);
        }
    }
}
