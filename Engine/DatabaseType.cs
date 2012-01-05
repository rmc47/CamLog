using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Engine
{
    [Obfuscation(Exclude=true)]
    internal enum DatabaseType
    {
        Unknown = 0,
        MySQL = 1,
    }
}
