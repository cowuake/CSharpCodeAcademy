using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.BaseLibrary
{
    public class Table
    {
        public ushort Number { get; }
        public byte MaxCustomers { get; private set; }
        public byte NActualCustomers { get; private set; }
        public DateTime? OpeningTime { get; private set; }
        public TimeSpan? OpeningTimeSpan
        {
            get
            {
                if (OpeningTime.HasValue)
                    return DateTime.Now - OpeningTime.Value;
                return null;
            }
        }
        public bool Available { get; set; }
        public IList<OrderEntry> Entries { get; }

        public Table(ushort number, byte maxCustomers, byte actualCustomers,
                     DateTime openingTime)
        {
            Number = number;
            MaxCustomers = maxCustomers;
            NActualCustomers = actualCustomers;
            OpeningTime = openingTime;
            Available = true;
            Entries = new List<OrderEntry>();
        }

        public bool Open(byte nCustomers)
        {
            if (nCustomers == 0 || nCustomers > MaxCustomers)
                //throw new ArgumentException(); // To be specialized
                return false;

            if (!Available)
                //throw new ArgumentException(); // To be changed (placeholder)
                return false;

            Available = false;
            NActualCustomers = nCustomers;
            OpeningTime = DateTime.Now;

            return true;
        }

        public bool AddCustomers(byte newCustomers)
        {
            if (newCustomers == 0)
                //throw new ArgumentException(); // To be specialized
                return false;

            if (NActualCustomers + newCustomers > MaxCustomers)
                //throw new ArgumentException(); // To be specialized
                return false;

            if (Available)
                //throw new ArgumentException(); // To be changed (placeholder)
                return false;

            NActualCustomers += newCustomers;

            return true;
        }

        public bool Close()
        {
            if (Available)
                //throw new ArgumentException(); // To be changed (placeholder)
                return false;

            NActualCustomers = 0;
            OpeningTime = null;

            if (Entries.Count > 0)
                Entries.Clear();

            return true;
        }

        //public OrderEntry EntryInOrder(MenuItem item)
        //{
        //    OrderEntry entry = Entries.FirstOrDefault(x => x.MenuItem == item);

        //    return (entry == null) ? new Entry
        //}

        public bool AddOrderEntry(MenuItem item, byte quantity)
        {
            if (Available)
                //throw new Exception($"Cannot take order from free table {Number}.");
                return false;

            OrderEntry existingEntry = Entries.FirstOrDefault(x => x.MenuItem == item);

            if (existingEntry == null)
            {
                Entries.Add(new OrderEntry(item, quantity));
            }
            else
            {
                existingEntry.Quantity += quantity;
            }

            return true;
        }

        public bool RemoveOrderEntry(MenuItem item, byte quantity)
        {
            if (quantity == 0)
                return false;

            OrderEntry existingEntry = Entries.FirstOrDefault(x => x.MenuItem == item);

            if (existingEntry == null)
                //throw new Exception($"Cannot remove entry from empty order!");
                return false;

            sbyte difference = (sbyte)(existingEntry.Quantity - quantity);

            if (difference < 0)
                //throw new Exception($"Impossible to remove more items than previously set!");
                return false;

            switch (difference)
            {
                case 0:
                    Entries.Remove(existingEntry);
                    break;
                default:
                    existingEntry.Quantity -= quantity;
                    break;
            }

            return true;
        }
    }
}
