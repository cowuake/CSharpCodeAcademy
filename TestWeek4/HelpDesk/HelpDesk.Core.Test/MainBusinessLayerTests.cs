using Xunit;
using HelpDesk.Core.ArchitecturalUtilities;
using HelpDesk.Core.BusinessLayer;
using HelpDesk.Core.Interface;
using HelpDesk.Core.Mock.DataRepository;

namespace HelpDesk.Core.Test;

public class MainBusinessLayerTests
{
    [Fact]
    public void InstantiateBusinessLayer()
    {
        DependencyInjector.Register<ITicketRepository, MockTicketRepository>();
        var bl = new MainBusinessLayer();
        Assert.NotNull(bl);
    }
}