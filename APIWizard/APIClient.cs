using APIWizard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard
{
    public class APIClient
    {
        private HttpClient httpClient;
        private WizardSchema schema;

        internal APIClient(TimeSpan pooledConnectionLifetime, WizardSchema schema)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = pooledConnectionLifetime
            };
            httpClient = new HttpClient(handler);

            this.schema = schema;
        }
    }
}
