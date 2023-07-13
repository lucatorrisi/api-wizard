using APIWizard.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard
{
    public interface IAPIClient
    {
        Task<TResult> DoRequestAsync<TResult>(string pathName, object requestBody, CancellationToken cancellationToken);
    }
}
