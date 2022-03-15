namespace Employees_DecoratorDP.Model.Benefits
{
    public class RestaurantTickets : IBenefit
    {
        public string TicketID { get; }
        public uint NTickets { get; }

        public RestaurantTickets(string ticketID, uint nTickets)
        {
            TicketID = ticketID;
            NTickets = nTickets;
        }

        public string GetPrintableDetails()
        {
            return $"\tRestaurant Ticket personal ID: {TicketID}\n" +
                $"\tNumber of tickets: {NTickets}";
        }
    }
}