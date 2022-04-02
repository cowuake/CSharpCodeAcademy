using HelpDesk.Client.Facilities;
using HelpDesk.Core.ArchitecturalUtilities;
using HelpDesk.Core.BusinessLayer;
using HelpDesk.Core.Interface;
using HelpDesk.Core.Mock.DataRepository;

namespace HelpDesk.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // MockTicketRepository is what we actually want to use
            DependencyInjector.Register<ITicketRepository, MockTicketRepository>();

            // Instantiate the business layer
            MainBusinessLayer bl = new MainBusinessLayer();

            // Instantiate the Command Line Interface
            CLI cli = new CLI(bl);

            // Run the interface
            cli.Run();
        }
    }
}
