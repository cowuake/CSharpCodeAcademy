using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Text;

namespace EasyConsoleFramework.Constants
{
    internal static class DICTIONARIES
    {
        internal static readonly IList<string> FIRST_NAMES =
            new List<string>()
            {
                "Arthur",
                "Elizabeth",
                "Janet",
                "Joe",
                "John",
                "Jordan",
                "Immanuel",
                "Nassim",
                "Nicholas",
                "Rachel",
                "Richard",
                "William",
            };

        internal static readonly IList<string> LAST_NAMES =
            new List<string>()
            {
                "Cameron",
                "Camus",
                "Halsey",
                "Kant",
                "Peterson",
                "Ryan",
                "Schopenhauer",
                "Scott",
                "Taleb",
                "Weyland"
            };
    }
}