using HelpDesk.Core.ArchitecturalUtilities;
using HelpDesk.Core.Interface;
using System;
using System.Collections.Generic;

namespace HelpDesk.Core.BusinessLayer
{
    public class MainBusinessLayer
    {
        private readonly ITicketRepository _ticketRepository;

        public MainBusinessLayer()
        {
            _ticketRepository =
                DependencyInjector.Resolve<ITicketRepository>();
        }

        public IEnumerable<Ticket> FetchAll()
        {
            return _ticketRepository.Fetch();
        }

        public Ticket GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Id");

            return _ticketRepository.GetById(id);
        }

        public List<Ticket> GetTicketsByState(TicketState state)
        {
            if (state == 0)
                return null;

            return _ticketRepository.GetByState(state);
        }

        public bool OpenTicket(Ticket ticket)
        {
            if (ticket == null)
                return false;

            return _ticketRepository.Add(ticket);
        }

        public bool Update(int id, TicketState state)
        {
            if (id <= 0)
                throw new ArgumentException();

            if (state == 0)
                throw new ArgumentException();

            Ticket ticket = _ticketRepository.GetById(id);
            ticket.State = state;

            if (Enum.GetName(typeof(TicketState), state) == "Closed" ||
                    Enum.GetName(typeof(TicketState), state) == "Canceled")
                ticket.ClosureDate = DateTime.Now;

            return true;
        }

        public bool RemoveTicket(Ticket ticket)
        {
            if (ticket == null)
                return false;

            return _ticketRepository.Delete(ticket);
        }
    }
}
