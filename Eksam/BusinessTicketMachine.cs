using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eksam
{
    class BusinessTicketMachine:EconomyTicketMachine
    {
        List<BusinessTicket> SoldBusinessTickets = new List<BusinessTicket>();
       
        public BusinessTicketMachine(string _flightCode, string _departure) : base(_flightCode, _departure)
        {
            flightCode = _flightCode;
            departure = _departure;
             rate1 = 0.2;
             rate2 = 0.26;
             rate3 = 0.34;
        }

        public override void PrintSoldTicketsInfo()
        {
            int ticketCounterForInfo = 1;
            foreach (BusinessTicket ticket in SoldBusinessTickets)
            {
                Console.WriteLine("Ticket {0}: \n", ticketCounterForInfo);
                ticket.PrintTicketInfo();
                Console.WriteLine(" ");
                ticketCounterForInfo++;
            }
            
        }

        public override void SaveTicketsInfoToFile()
        {
            using (StreamWriter sw = new StreamWriter("TicketInfo.txt"))
            {
                int ticketCounterForInfo = 1;
                foreach (BusinessTicket ticket in SoldBusinessTickets)
                {
                    sw.WriteLine("Ticket {0}: \n", ticketCounterForInfo);
                    sw.WriteLine("Name: {0}\r\nFlight date: {1}\r\nPrice: {2}\r\nFrom: {3} to {4}\r\nTicket type: {5}\r\nSeat number: {6}\r\nHave a nice flight!",
                    ticket.name, ticket.date, ticket.ticketPrice, ticket.fromCity, ticket.toCity, ticket.ticketType, ticket.seatNr);
                    sw.WriteLine(" ");
                    ticketCounterForInfo++;
                }
            }
        }
        public override void PrintEmptySeats()
        {
            emptySeats = nrOfSeats - SoldBusinessTickets.Count;
            Console.WriteLine("Number of empty seats: {0}", emptySeats);
        }

        public override void SellTicket(string _name)
        {
            emptySeats = nrOfSeats - SoldBusinessTickets.Count;
            if (emptySeats == 0)
            {
                Console.WriteLine("Plane is full!");
            }
            else
            {
                if (flightCode[flightCode.Length-1] == 'b')
                {
                    CreateTicket(_name);
                }
                else
                {
                    Console.WriteLine("Cannot sell ticket, no business class");
                }
            }
        }

        internal override EconomyTicket CreateTicket(string _name)
        {
            BusinessTicket b = new BusinessTicket();
            b.name = _name;
            flightDate = DateTime.Parse(departure);
            b.date = flightDate;
            ApplyTimeRate(finalPrice);
            ApplyOccupancyRate(finalPrice);
            b.ticketPrice = finalPrice;
            GetFirstCity();
            GetSecondCity();
            SetDirection();
            b.fromCity = from;
            b.toCity = to;
            b.seatNr = ticketCounter;
            SoldBusinessTickets.Add(b);
            ticketCounter++;
            b.PrintTicketInfo();

            return b;
        }

        internal override double ApplyOccupancyRate(double price)
        {
          

            occupancyRate = SoldBusinessTickets.Count / nrOfSeats * 100;


            if (occupancyRate >= 26 && occupancyRate < 51)
            {
                finalPrice = price + price * rate1;
            }
            else if (occupancyRate >= 51 && occupancyRate < 76)
            {
                finalPrice = price + price * rate2;
            }
            else if (occupancyRate >= 76 && occupancyRate <= 100)
            {
                finalPrice = price + price * rate3;
            }
            else
            {
                finalPrice = price;
            }

            return finalPrice;
        }


    }
}
