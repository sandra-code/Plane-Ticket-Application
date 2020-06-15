using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eksam
{
    class EconomyTicketMachine
    {
        public string flightCode, departure, cityA, cityB, from, to;
        public int basePrice, nrOfSeats, emptySeats;
        public double finalPrice, occupancyRate;
        public double rate1 = 0.1;
        public double rate2 = 0.13;
        public double rate3 = 0.17;
        public int ticketCounter = 1;
        public DateTime flightDate;

        List<EconomyTicket> SoldEconomyTickets = new List<EconomyTicket>();
        
        public EconomyTicketMachine(string _flightCode, string _departure)
        {
            flightCode = _flightCode;
            departure = _departure;
        }

        public void SetPriceData(int _price, int _nrOfSeats)
        {
            basePrice = _price;
            nrOfSeats = _nrOfSeats;
        }

        public virtual void PrintSoldTicketsInfo()
        {
            int ticketCounterForInfo = 1;
            foreach (EconomyTicket ticket in SoldEconomyTickets)
            {
                Console.WriteLine("Ticket {0}: \n", ticketCounterForInfo);
                ticket.PrintTicketInfo();
                Console.WriteLine(" ");
                ticketCounterForInfo++;
            }
        }

        public virtual void SaveTicketsInfoToFile()
        {
            using(StreamWriter sw = new StreamWriter("TicketInfo.txt"))
            {
                int ticketCounterForInfo = 1;
                foreach (EconomyTicket ticket in SoldEconomyTickets)
                {
                    sw.WriteLine("Ticket {0}: \n", ticketCounterForInfo);
                    sw.WriteLine("Name: {0}\r\nFlight date: {1}\r\nPrice: {2}\r\nFrom: {3} to {4}\r\nTicket type: {5}\r\nSeat number: {6}\r\nHave a nice flight!",
                    ticket.name, ticket.date, ticket.ticketPrice, ticket.fromCity, ticket.toCity, ticket.ticketType, ticket.seatNr);
                    sw.WriteLine(" ");
                    ticketCounterForInfo++;
                }
            }
        }

        public virtual void PrintEmptySeats()
        {
            emptySeats = nrOfSeats - SoldEconomyTickets.Count;
            Console.WriteLine("Number of empty seats: {0}", emptySeats);
        }

        public virtual void SellTicket(string _name)
        {
            emptySeats = nrOfSeats - SoldEconomyTickets.Count;
            if (emptySeats == 0)
            {
                Console.WriteLine("Plane is full!");
            }
            else
            {
                CreateTicket(_name);
            }
            
        }

        internal virtual EconomyTicket CreateTicket(string _name)
        {
            EconomyTicket t = new EconomyTicket();
            t.name = _name;
            SetFlightDate();
            t.date = flightDate;
            ApplyWeekdayRate(basePrice);
            ApplyTimeRate(finalPrice);
            ApplyOccupancyRate(finalPrice);
            t.ticketPrice = finalPrice;
            GetFirstCity();
            GetSecondCity();
            SetDirection();
            t.fromCity = from;
            t.toCity = to;
            t.seatNr = ticketCounter;
            SoldEconomyTickets.Add(t);
            ticketCounter++;
            t.PrintTicketInfo();
            

            return t;
        }

        //FOR TESTING

        public void SellTicket(string _name, DateTime _buyingTime)
        {
            EconomyTicket t = new EconomyTicket();
            t.name = _name;
            SetFlightDate();
            t.date = flightDate;
            ApplyWeekdayRate(basePrice);
            ApplyTimeRateForTestingOnly(finalPrice, _buyingTime);
            ApplyOccupancyRate(finalPrice);
            t.ticketPrice = finalPrice;
            GetFirstCity();
            GetSecondCity();
            SetDirection();
            t.fromCity = from;
            t.toCity = to;
            t.seatNr = ticketCounter;

            emptySeats = nrOfSeats - SoldEconomyTickets.Count;
            if (emptySeats == 0)
            {
                Console.WriteLine("Plane is full!");
            }
            else
            {
                SoldEconomyTickets.Add(t);
                ticketCounter++;
                t.PrintTicketInfo();
            }
        }
      
        //Finding the price
    
        internal double ApplyWeekdayRate(double price)
        {

            if(flightDate.DayOfWeek==DayOfWeek.Friday || flightDate.DayOfWeek == DayOfWeek.Saturday)
            {
                finalPrice = price+ price * 0.15;
            }
            else
            {
                finalPrice = price;
            }

            return finalPrice;
        }

        public double ApplyTimeRate(double price)
        {
            int gap = flightDate.Month - DateTime.Now.Month;
            if (gap < 6)
            {
                finalPrice = price+price*((6 - gap) * 0.1);
            }
            else
            {
                finalPrice = price;
            }
            return finalPrice;
        }

        public double ApplyTimeRateForTestingOnly(double price, DateTime buyingTime)
        {
            int gap = flightDate.Month - buyingTime.Month;
            if (gap < 6)
            {
                finalPrice = price + price * ((6 - gap) * 0.1);
            }
            else
            {
                finalPrice = price;
            }
            return finalPrice;
        }



        internal virtual double ApplyOccupancyRate(double price)
        {
            occupancyRate = SoldEconomyTickets.Count / nrOfSeats * 100;
            

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

        
        //decoding the flight code
        public string GetFirstCity()
        {
            string cityAcode = flightCode.Substring(2, 3);
            
            switch (cityAcode)
            {
                case "TYO":
                    cityA = "Tokyo";
                    break;
                case "TLL":
                    cityA = "Tallinn";
                    break;
                case "BER":
                    cityA = "Berlin";
                    break;
                case "KBP":
                    cityA = "Kiev";
                    break;
                case "KRS":
                    cityA = "Kuressaare";
                    break;
            }
            return cityA;
        }

        public string GetSecondCity()
        {
            string cityBcode = flightCode.Substring(5, 3);

            switch (cityBcode)
            {
                case "TYO":
                    cityB = "Tokyo";
                    break;
                case "TLL":
                    cityB = "Tallinn";
                    break;
                case "BER":
                    cityB = "Berlin";
                    break;
                case "KBP":
                    cityB = "Kiev";
                    break;
                case "KRS":
                    cityB = "Kuressaare";
                    break;
            }

            return cityB;
        }

        public void SetDirection()
        {
            if (flightCode[8] == '2')
            {
                from = cityA;
                to = cityB;
            }
            else if (flightCode[8] == '5')
            {
                from = cityB;
                to = cityA;
            }
            
        }

        public DateTime SetFlightDate()
        {
            try
            {
                flightDate = DateTime.Parse(departure);
            }
            catch
            {
                Console.WriteLine("Flight date is in the wrong format, please use this format: year-month-day or year/month/day");
            }
            return flightDate;
        }


    }
}
