using APIWizard.Constants;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using APIWizard.Enums;
using APIWizard.Models.Interfaces;
using APIWizard.Models.Configuration;
using APIWizard.Utils;

namespace APIWizard.Builders
{
    /// <summary>
    /// Builder class for creating an instance of IAPIClient.
    /// </summary>
    public class APIClientBuilder
    {
        private IWizardSchema? schema;
        private IConfigurationSection? configurationSection;
        private OpenAPIVersion openApiVersion;
        private ConfigurationType configurationType;
        private string? jsonSchema;

        public APIWizardOptions options = new();

        /// <summary>
        /// Sets the configuration file path.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        /// <returns>The current APIClientBuilder instance.</returns>
        public APIClientBuilder WithConfigurationFile(string path)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(path, nameof(path), ExceptionMessages.InvalidFilePath);

            string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location) ?? string.Empty, path);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"{ExceptionMessages.ConfigurationFileNotFound} {fullPath}", path);
            }

            try
            {
                jsonSchema = File.ReadAllText(fullPath);
                configurationType = ConfigurationType.Json;
                return this;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ExceptionMessages.FailedReadConfigurationFile} {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sets the configuration section.
        /// </summary>
        /// <param name="section">The IConfigurationSection instance.</param>
        /// <returns>The current APIClientBuilder instance.</returns>
        public APIClientBuilder WithConfiguration(IConfigurationSection section)
        {
            ValidationUtils.ArgumentNotNull(section, nameof(section), ExceptionMessages.ConfigurationSectionNull);
            configurationSection = section;
            configurationType = ConfigurationType.Section;
            return this;
        }

        /// <summary>
        /// Sets the OpenAPI URL configuration.
        /// </summary>
        /// <param name="jsonOpenApiUrl">The URL of the JSON Open API.</param>
        /// <returns>The current APIClientBuilder instance.</returns>
        public APIClientBuilder WithOpenAPIUrlConfigurationAsync(string jsonOpenApiUrl)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(jsonOpenApiUrl, nameof(jsonOpenApiUrl), ExceptionMessages.InvalidOpenAPIConfigurationUrl);

            using var httpClient = new HttpClient();
            try
            {
                jsonSchema = httpClient.GetStringAsync(jsonOpenApiUrl).Result;
                configurationType = ConfigurationType.Json;
                return this;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ExceptionMessages.FailedRetrieveRemoteConfig} {ex.Message}", ex);
            }
        }


        /// <summary>
        /// Sets the OpenAPI version.
        /// </summary>
        /// <param name="openApiVersion">The OpenAPI version.</param>
        /// <returns>The current APIClientBuilder instance.</returns>
        public APIClientBuilder WithVersion(OpenAPIVersion openApiVersion = OpenAPIVersion.V2)
        {
            this.openApiVersion = openApiVersion;
            return this;
        }

        /// <summary>
        /// Sets additional options for the APIClient.
        /// </summary>
        /// <param name="action">An action to configure the options.</param>
        /// <returns>The current APIClientBuilder instance.</returns>
        public APIClientBuilder WithOptions(Action<APIWizardOptions> action)
        {
            action(options);
            return this;
        }

        /// <summary>
        /// Builds an instance of IAPIClient.
        /// </summary>
        /// <returns>An instance of IAPIClient.</returns>
        public IAPIClient Build()
        {
            if (configurationType == ConfigurationType.Json && !string.IsNullOrEmpty(jsonSchema))
            {
                schema = openApiVersion switch
                {
                    OpenAPIVersion.V2 => DeserializeSchemaFromJson<Models.V2.WizardSchema>(jsonSchema),
                    OpenAPIVersion.V3 => DeserializeSchemaFromJson<Models.V3.WizardSchema>(jsonSchema),
                    _ => throw new ArgumentException(ExceptionMessages.InvalidOpenAPIVersion),
                };
            }
            else if (configurationType == ConfigurationType.Section && configurationSection != null)
            {
                schema = openApiVersion switch
                {
                    OpenAPIVersion.V2 => DeserializeSchemaFromSection<Models.V2.WizardSchema>(configurationSection),
                    OpenAPIVersion.V3 => DeserializeSchemaFromSection<Models.V3.WizardSchema>(configurationSection),
                    _ => throw new ArgumentException(ExceptionMessages.InvalidOpenAPIVersion),
                };
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidConfiguration);
            }

            schema?.AddServers(options.AdditionalServers);

            return schema != null
                ? new APIClient(options, schema)
                : throw new InvalidOperationException(ExceptionMessages.SchemaIsNull);
        }


        private static T DeserializeSchemaFromJson<T>(string json) where T : IWizardSchema
        {
            ValidationUtils.ArgumentNotNullOrEmpty(json, nameof(json), ExceptionMessages.InvalidJson);

            return JsonConvert.DeserializeObject<T>(json)
                ?? throw new ArgumentException(ExceptionMessages.InvalidConfigurationFileFormat, nameof(json));
        }

        private static T? DeserializeSchemaFromSection<T>(IConfigurationSection section) where T : class, IWizardSchema
        {
            ValidationUtils.ArgumentNotNull(section, nameof(section));

            return section.Get<T>(options =>
            {
                options.BindNonPublicProperties = true;
            });
        }
    }
}
