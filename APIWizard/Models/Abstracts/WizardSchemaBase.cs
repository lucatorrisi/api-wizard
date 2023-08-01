using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Models.Interfaces;
using APIWizard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models.Abstracts
{
    internal abstract class WizardSchemaBase
    {
        internal abstract Uri GetUri(string route);
        internal abstract Uri GetUri(string server, string route);
    }
}
