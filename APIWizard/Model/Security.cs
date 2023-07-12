using APIWizard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Model
{
    internal class Security
    {
        public SecurityType Type { get; set; }
        public string Key { get; set; }
    }
}
