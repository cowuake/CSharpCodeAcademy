using EasyConsoleFramework;
using Library.ConsoleClient.Methods;
using Library.WCF.ServiceLibrary;
using PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Library.ConsoleClient
{
    internal class Program
    {
        // SEE: https://stackoverflow.com/questions/34073467/ansi-coloring-console-output-with-net
        static bool TryEnableAnsiCodesForHandle(Kernel32.StdHandle stdHandle)
        {
            var consoleHandle = Kernel32.GetStdHandle(stdHandle);
            if (Kernel32.GetConsoleMode(consoleHandle, out var consoleBufferModes) &&
                consoleBufferModes.HasFlag(Kernel32.ConsoleBufferModes.ENABLE_VIRTUAL_TERMINAL_PROCESSING))
                return true;

            consoleBufferModes |= Kernel32.ConsoleBufferModes.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            return Kernel32.SetConsoleMode(consoleHandle, consoleBufferModes);
        }

        static void Main(string[] args)
        {
            // Only needed since this application is using .NET Framework 4.8 instead of .NET Core 3.1
            TryEnableAnsiCodesForHandle(Kernel32.StdHandle.STD_OUTPUT_HANDLE);

            CLI cli = new CLI();

            cli.SetApplicationName("LIBRARY WCS->CLI CLIENT");

            using (var host = new ServiceHost(typeof(LibraryService)))
            {
                host.Open();
            }

            cli.Run();
        }
    }
}