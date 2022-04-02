using System;
using System.Text;

namespace HelpDesk.Core
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set;  }
        public string Customer { get; set;  }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public TicketCategory Category { get; set;  }
        public TicketState State { get; set; }
        public string Assignee { get; set;  }
        public DateTime? ClosureDate { get; set; }

        public Ticket() { }

        public Ticket(int id, string assignee, TicketCategory category, string customer,
                        string shortDescription, string longDescription = null)
        {
            Id = id;
            Assignee = assignee;
            Category = category;
            Customer = customer;
            ShortDescription = shortDescription;

            // Not using null in order to avoid null reference exceptions here and there
            LongDescription = longDescription ?? string.Empty;

            CreationDate = DateTime.Now;
            State = TicketState.Open;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Ticket Id: {Id}");
            sb.AppendLine($"State: {State}");
            sb.AppendLine($"Assigned to: {Assignee}");
            sb.AppendLine($"Category: {Category}");
            sb.AppendLine($"Customer: {Customer}");
            sb.AppendLine($"Short description: {ShortDescription}");

            if (!String.IsNullOrEmpty(LongDescription))
                sb.AppendLine($"Long Description: {LongDescription}");

            if (ClosureDate != null)
                sb.AppendLine($"Closure date: {ClosureDate:d}");

            return sb.ToString();
        }
    }

    /// <summary>
    /// byte enum for storing available ticket categories
    /// </summary>
    public enum TicketCategory : byte
    {
        Docker = 1,
        Kubernetes = 2,
        SSL = 3,
        LinuxKernelPanic = 4,
        GRUB = 5,
        VirtualBox = 6,
        VMWare = 7,
        Hardware = 8,
        Edge = 9,
        Office365 = 10,
        Powershell = 11,
        VisualStudio = 12,
        VisualStudioCode = 13,
        WindowsTerminal = 14,
        WSL = 15,
        Security = 16
    }

    /// <summary>
    /// byte enum for storing available ticket states
    /// </summary>
    public enum TicketState : byte
    {
        Open = 1,       // The ticket has to be dealt with
        Canceled = 2,   // Ticket canceled due to illegal request or already solved by the customer
        Resolved = 3,   // The problem has been solved, waiting for customer confirmation
        Closed = 4,     // Ticket effectively closed
    }
}
