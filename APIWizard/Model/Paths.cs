using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Model
{
    internal class Paths
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string[] Consumes { get; set; }
        public Security Security { get; set; }
    }
}
