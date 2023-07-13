using System;
using System.Collections;
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
        public ICollection<string> Schemes { get; set; }
        public ICollection<Path> Paths { get; set; }
    }
}
