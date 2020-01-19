using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Cecil
{
    class Registry
    {
        internal static LocalMachine LocalMachine;

        public static string GetValue (string hive, string key, object def) => null;
    }

    class LocalMachine
    {
        public string Name { get; set; }
    }
}
