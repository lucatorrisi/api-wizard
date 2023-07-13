using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Exceptions
{
    public class APIClientException : Exception
    {
        public APIClientException()
        {
        }

        public APIClientException(string message)
            : base(message)
        {
        }

        public APIClientException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
