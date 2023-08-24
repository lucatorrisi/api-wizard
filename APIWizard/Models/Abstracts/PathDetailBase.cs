using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models.Abstracts
{
    internal abstract class PathDetailBase
    {
        internal abstract string? GetContentType();
        internal abstract bool HasBodyParameter();
        internal abstract bool IsBodyRequired();
        internal abstract void AddDummyBodyParam();
    }
}
