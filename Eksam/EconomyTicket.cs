using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksam
{
    class EconomyTicket : ITicket
    {

        public string name, fromCity, toCity;
        public double ticketPrice;
        public int ticketType, seatNr;
        public DateTime?date;
        public EconomyTicket()
        {
            name = "";
            date = null;
            fromCity = "";
            toCity = "";
            ticketPrice = 0;
            ticketType = 2;
            seatNr=0;
        }

        
        public void PrintTicketInfo()
        {
            Console.WriteLine("Name: {0}\nFlight date: {1}\nPrice: {2}\nFrom: {3} to {4}\nTicket type: {5}\nSeat number: {6}\nHave a nice flight!", 
                this.name, this.date, this.ticketPrice, this.fromCity,this.toCity,this.ticketType,this.seatNr);
            Console.WriteLine(" ");
        }
    }
}
