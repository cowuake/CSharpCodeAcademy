using ConnectionStringChecker.Core;
using EasyConsoleFramework;

namespace ConnectionStringChecker
{
    internal class Program
    {
        internal static void Main()
        {
            CLI cli = new CLI();
            
            cli.SetApplicationName("CONNECTION STRING CHECKER");
            
            cli.AddAction("CLI", "Insert connection string through CLI", Methods.CheckConnectionFromCLI);
            cli.AddAction("ASCII", "Read one or more connections string from ASCII file", Methods.CheckConnectionFromCLI);
            
            cli.RunAsync();
        }
    }
}