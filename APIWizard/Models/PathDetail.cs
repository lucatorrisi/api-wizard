using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models
{
    internal class PathDetail
    {
        public List<string> Consumes { get; set; }
        public List<string> Produces { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
}
