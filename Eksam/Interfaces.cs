using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksam
{
    interface ITicket
    {
        void PrintTicketInfo();
    }

    interface ITicketMachine
    {
        void SetPriceData(int _price, int _nrOfSeats);
        void PrintSoldTicketsInfo();
        void SaveTicketsInfoToFile();
        void PrintEmptySeats();
        void SellTicket(string _name);
        void SellTicket(string _name, DateTime _buyingTime);
        double ApplyWeekdayRate(double price);
        double ApplyTimeRate(double price);
        double ApplyTimeRateForTestingOnly(double price, DateTime buyingTime);
        double ApplyOccupancyRate(double price);
        void GetFirstCity();
        void GetSecondCity();
        void SetDirection();
        DateTime SetFlightDate();
    }
}
