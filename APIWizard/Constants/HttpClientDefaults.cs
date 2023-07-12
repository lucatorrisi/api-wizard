using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Constants
{
    public static class HttpClientDefaults
    {
        public static TimeSpan PooledConnectionLifetime = TimeSpan.FromMinutes(2);
    }
}
