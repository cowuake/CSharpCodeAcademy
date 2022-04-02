using System.Collections.Generic;
using System.Linq;
using HelpDesk.Core.Interface;

namespace HelpDesk.Core.Mock.DataRepository
{
    public class MockTicketRepository : ITicketRepository
    {
        public bool Add(Ticket entity)
        {
            if (entity == null)
                return false;

            var newId = PersistentData.Instance.Tickets.Max(t => t.Id) + 1;
            entity.Id = newId;

            PersistentData.Instance.Tickets.Add(entity);

            return true;
        }

        public bool Delete(Ticket entity)
        {
            if (entity == null)
                return false;

            return PersistentData.Instance.Tickets.Remove(entity);
        }

        public IEnumerable<Ticket> Fetch()
        {
            return PersistentData.Instance.Tickets;
        }

        public Ticket GetById(int id)
        {
            if (id <= 0)
                return null;

            return PersistentData.Instance.Tickets.Find(s => s.Id == id);
        }

        public List<Ticket> GetByState(TicketState state)
        {
            if (state == 0)
                return null;

            return PersistentData.Instance.Tickets.Where(x => x.State == state).ToList();
        }

        public bool Update(Ticket entity)
        {
            return true;
        }
    }
}
