using System;
using System.Collections.Generic;
using System.Text;

namespace MainLibrary
{
    public static class CLIUtils
    {
        public static void WaitForExit()
        {
            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
