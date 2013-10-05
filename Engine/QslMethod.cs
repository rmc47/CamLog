using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Engine
{
    [Obfuscation(Exclude=true)]
    public enum QslMethod
    {
        Unknown = 0,
        Bureau,
        Direct,
    }
}
