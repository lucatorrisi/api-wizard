using APIWizard.Constants;
using APIWizard.Enums;
using APIWizard.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Builders
{
    public class APIClientBuilder
    {
        private WizardSchema schema;

        public APIClientBuilder WithConfigurationFile(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            schema = JsonConvert.DeserializeObject<WizardSchema>(File.ReadAllText(path)) ?? throw new ArgumentException(ExceptionMessages.APIClientBuilderWithConfigurationFilePathArgumentException, path);
            return this;
        }
        public APIClientBuilder WithConfiguration(IConfigurationSection section)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            schema = new WizardSchema();
            section.Bind(schema);
            return this;
        }
        public APIClient Build()
        {
            return new APIClient(HttpClientDefaults.PooledConnectionLifetime, schema);
        }
    }
}
