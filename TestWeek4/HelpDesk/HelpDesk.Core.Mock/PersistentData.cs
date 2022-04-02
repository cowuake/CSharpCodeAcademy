using System;
using System.Collections.Generic;
using HelpDesk.Core;

namespace HelpDesk.Core.Mock
{
    public class PersistentData
    {
        private readonly static Lazy<PersistentData> _persistentData =
            new Lazy<PersistentData>(() => new PersistentData());

        public static PersistentData Instance => _persistentData.Value;

        public List<Ticket> Tickets = new List<Ticket>
        {
            //new Ticket(1, "Gregory Freeman", TicketCategory.Docker, "Paul Walter", "Cannot restart container"),
            //new Ticket(2, "Gregory Freeman", TicketCategory.WSL, "Paul Walter", "Cannot find Fedora image"),
            //new Ticket(3, "Gregory Freeman", TicketCategory.Kubernetes, "Walter White", "Not enough Chemistry"),
            //new Ticket(4, "John Sullivan", TicketCategory.Powershell, "Penelope Garcia", "Cannot change terminal color theme")

            new Ticket
            {
                Id = 1,
                CreationDate = DateTime.Now,
                Customer = "Paul Walter",
                ShortDescription = "Cannot restart container",
                LongDescription = String.Empty,
                Category = TicketCategory.Docker,
                State = TicketState.Closed,
                Assignee = "Gregory Freeman",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 2,
                CreationDate = DateTime.Now,
                Customer = "Paul Walter",
                ShortDescription = "Cannot find Fedora image",
                LongDescription = String.Empty,
                Category = TicketCategory.WSL,
                State = TicketState.Open,
                Assignee = "Gregory Freeman",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 3,
                CreationDate = DateTime.Now,
                Customer = "Walter White",
                ShortDescription = "Not enough Chemistry",
                LongDescription = "Mr. Walter White is seriously concerned with at least some tons of Chemistry being everywhere",
                Category = TicketCategory.Kubernetes,
                State = TicketState.Canceled,
                Assignee = "Gregory Freeman",
                ClosureDate = DateTime.Now
            },
            new Ticket
            {
                Id = 4,
                CreationDate = DateTime.Now,
                Customer = "John D. Carmack",
                ShortDescription = "I want to code all night but fans are noisy",
                LongDescription = "I want some Ryzen CPUs since the present i9 CPUs I'm using have noisy fans",
                Category = TicketCategory.Hardware,
                State = TicketState.Open,
                Assignee = "AssistantX",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 5,
                CreationDate = DateTime.Now,
                Customer = "Richard Stallman",
                ShortDescription = "Give me another machine without Windows aboard",
                LongDescription = "I've received a machine with proprietary bootloader and operating system",
                Category = TicketCategory.Security,
                State = TicketState.Resolved,
                Assignee = "AssistantX",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 6,
                CreationDate = DateTime.Now,
                Customer = "Frank Spielberg",
                ShortDescription = "Cannot run my Linux box",
                LongDescription = "The kernel keeps panicking every time I compile code with Cargo",
                Category = TicketCategory.LinuxKernelPanic,
                State = TicketState.Open,
                Assignee = "Roger Sterling",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 7,
                CreationDate = DateTime.Now,
                Customer = "Jerry Burton",
                ShortDescription = "Projects created in VS not restoring in Code",
                LongDescription = "VSCode does not correctly restore projects previously created in VS",
                Category = TicketCategory.VisualStudioCode,
                State = TicketState.Open,
                Assignee = "Roger Sterling",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 8,
                CreationDate = DateTime.Now,
                Customer = "Jerry Burton",
                ShortDescription = "Not able to install Chocolatey",
                LongDescription = "I cannot install Chocolatey since I do not have write access to install folder",
                Category = TicketCategory.Powershell,
                State = TicketState.Open,
                Assignee = "Roger Sterling",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 9,
                CreationDate = DateTime.Now,
                Customer = "Jessie Hartman",
                ShortDescription = "Office365 who?",
                LongDescription = "They say I should use Office365 but, well... What's that supposed to be?",
                Category = TicketCategory.Office365,
                State = TicketState.Open,
                Assignee = "AssistantX",
                ClosureDate = null
            },
            new Ticket
            {
                Id = 10,
                CreationDate = DateTime.Now,
                Customer = "Ralph Costello",
                ShortDescription = "Hardware acceleration not working in VMWare Player",
                LongDescription = "I'm not able to run heavy 3D applications when running Windows 7 inside a VM",
                Category = TicketCategory.VMWare,
                State = TicketState.Open,
                Assignee = "AssistantX",
                ClosureDate = null
            },
        };
    }
}