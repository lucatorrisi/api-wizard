using APIWizard.Constants;
using APIWizard.Enums;
using APIWizard.Extensions;
using APIWizard.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Path = APIWizard.Model.Path;

namespace APIWizard.Builders
{
    public class APIClientBuilder
    {
        private WizardSchema schema;

        public APIClientBuilder WithConfigurationFile(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            schema = JsonConvert.DeserializeObject<WizardSchema>(File.ReadAllText(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path))) ?? throw new ArgumentException(ExceptionMessages.APIClientBuilderWithConfigurationFilePathArgumentException, path);
            return this;
        }
        public APIClientBuilder WithConfiguration(IConfigurationSection section)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            schema = new WizardSchema();
            section.Bind(schema);
            return this;
        }
        public IAPIClient Build()
        {
            return new APIClient(HttpClientDefaults.PooledConnectionLifetime, schema, CreateRequestsDictionary());
        }

        private Dictionary<string, HttpRequestMessage> CreateRequestsDictionary()
        {
            Dictionary<string, HttpRequestMessage> requests = new Dictionary<string, HttpRequestMessage>();

            foreach (Path path in schema.Paths)
            {
                requests.TryAdd(path.Name, path.ToHttpRequestMessage(schema.Host, schema.BasePath, schema.Schemes));
            }

            return requests;
        }
    }
}
