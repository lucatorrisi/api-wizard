using APIWizard.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models.Configuration
{
    public class APIWizardOptions
    {
        public TimeSpan PooledConnectionLifetime { get; set; } = HttpClientDefaults.PooledConnectionLifetime;
    }
}
