using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing: when the flight date is in the wrong format");
            EconomyTicketMachine etm = new EconomyTicketMachine("EATLLTYO2", "2018-kaks-02");
            etm.SetPriceData(100, 3);
            etm.SellTicket("Tim Jones");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: when plane is full");
            EconomyTicketMachine etm1 = new EconomyTicketMachine("EATLLTYO2", "2018-02-02");
            etm1.SetPriceData(100, 3);
            etm1.SellTicket("Tim Jones");
            etm1.SellTicket("Mary Smith");
            etm1.SellTicket("Alex Stone");
            etm1.SellTicket("Rachel Rose");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: business class cannot be created");
            BusinessTicketMachine btm1 = new BusinessTicketMachine("EATLLTYO2", "2018-02-02");
            btm1.SetPriceData(100, 3);
            btm1.SellTicket("Tim Jones");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: writing info to .txt file");
            EconomyTicketMachine etm2 = new EconomyTicketMachine("EATLLTYO2", "2018-02-02");
            etm2.SetPriceData(100, 5);
            etm2.SellTicket("Tim Jones");
            etm2.SellTicket("Mary Smith");
            etm2.SellTicket("Alex Stone");
            etm2.SellTicket("Rachel Rose");
            etm2.SaveTicketsInfoToFile();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: printing empty seats");
            BusinessTicketMachine btm2 = new BusinessTicketMachine("EATLLTYO2b", "2018-02-02");
            btm2.SetPriceData(100, 5);
            btm2.SellTicket("Tim Jones");
            btm2.SellTicket("Mary Smith");
            btm2.SellTicket("Alex Stone");
            btm2.SellTicket("Rachel Rose");
            btm2.PrintEmptySeats();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");


            Console.WriteLine("Testing: printing info about all sold tickets");
            EconomyTicketMachine etm3 = new EconomyTicketMachine("EATLLTYO2b", "2018-02-02");
            etm3.SetPriceData(100, 5);
            etm3.SellTicket("Tim Jones");
            etm3.SellTicket("Mary Smith");
            etm3.SellTicket("Alex Stone");
            etm3.SellTicket("Rachel Rose");
            etm3.PrintSoldTicketsInfo();
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: Selling 60 tickets with economy ticket machine // DIFFERENT MONTHS");
            Random rndMonths = new Random();
            string[] names = new string[] {"Mari Maasikas","Kalle Kadakas","Riina Roos","Juku Jalakas","Tim Timukas","Peeter Pikk","Sille Suits",
                "Lauri Laudas","Pille Pihlakas","Heino Hall"};
            int[] months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            
            EconomyTicketMachine etm4 = new EconomyTicketMachine("EATLLTYO2b", "2018-05-02");
            etm4.SetPriceData(100, 75);
            Random rnd = new Random();
            for (int i = 0; i < 61; i++)
            {
                int monthInt = months[rndMonths.Next(months.Count())];
                DateTime dt = new DateTime(2018, monthInt, 03);
                string s = names[rnd.Next(names.Length)];
                etm4.SellTicket(s,dt);
            }
            etm4.SaveTicketsInfoToFile();

            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            Console.WriteLine("Testing: Selling 60 tickets with business ticket machine");
           
            BusinessTicketMachine btm3 = new BusinessTicketMachine("EATLLTYO2b", "2018-05-02");
            btm3.SetPriceData(50, 61);
            Random rnd2 = new Random();
            for (int i = 0; i < 61; i++)
            {
                string s = names[rnd2.Next(names.Length)];
                btm3.SellTicket(s);
            }

            



            Console.ReadLine();
        }
    }
}
