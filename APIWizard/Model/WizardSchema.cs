using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Model
{
    internal class WizardSchema
    {
        public string Host { get; set; }
        public string BasePath { get; set; }
        public string[] Schemes { get; set; }
        public Paths[] Paths { get; set; }
    }
}
