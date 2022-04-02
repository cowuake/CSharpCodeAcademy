using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core.Interface
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        //public Ticket GetById (int id);
        public List<Ticket> GetByState (TicketState state);
    }
}
