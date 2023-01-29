using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
    internal class Program
    {
        // Define a structure to hold booking information
        struct Booking
        {
            public int roomNumber;
            public string customerFirstName;
            public string customerLastName;
            public bool roomBooked;
        }
        static int mainMenu()
        {
            int menuOption = 0;
                       
            Console.WriteLine("Please select an option from below: ");
            Console.WriteLine("");
            Console.WriteLine("1. Import bookings");
            Console.WriteLine("2. View Checkins");
            Console.WriteLine("3. Check in customer");
            Console.WriteLine("4. Check out customer");
            Console.WriteLine("5. Save Checkins");
            Console.WriteLine("6. Exit");
           
            try
            {
                menuOption = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("You have entered an invalid menu option, please try again!");
                return 7;
            }

            //Return user to main menu if they select a value that is not an option

            if (menuOption <= 0 || menuOption >= 7)
            {
                Console.WriteLine("You have entered an invalid menu option, please try again!");
                return 7;

            }

            return menuOption;
        }
     
        static void Checkin(Booking []rooms)
        {
            Console.WriteLine("The following rooms are avaliable:");
            for (int i = 0; i < 20; i++)
            {
                if (rooms[i].roomBooked == false)
                {
                    Console.WriteLine("Room: " + rooms[i].roomNumber);
                }
            }

            Console.WriteLine("Please enter a room number to book: ");
            int roomNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter customers first name:");
            String firstName = Console.ReadLine();
            Console.WriteLine("Please enter the customers last name:");
            String lastName = Console.ReadLine();

            rooms[roomNum - 1].roomBooked = true;
            rooms[roomNum - 1].customerLastName = lastName;
            rooms[roomNum - 1].customerFirstName = firstName;

            Console.WriteLine("Room " + roomNum + " is now booked");
        }

        static void viewCheckins(Booking[] rooms)
        {
            Console.WriteLine("The following bookings have been made:");
            for (int i = 0; i < 20; i++)
            {
                if (rooms[i].roomBooked == true)
                {
                    Console.WriteLine("Room: " + rooms[i].roomNumber);
                    Console.WriteLine("Customer first name: " + rooms[i].customerFirstName);
                    Console.WriteLine("Customer last name: " + rooms[i].customerLastName);
                }
            }
        }

        static void importCheckins(Booking[] rooms)
        {
            String fileName = @"C:\Temp\hotelBookings.txt";
            
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (reader.Peek() >= 0)
                {
                    String nextLine = reader.ReadLine();
                    String[] roomDetails = nextLine.Split(';');
                    rooms[Convert.ToInt32(roomDetails[0])].roomBooked = true;
                    rooms[Convert.ToInt32(roomDetails[0])].customerFirstName = roomDetails[1];
                    rooms[Convert.ToInt32(roomDetails[0])].customerLastName = roomDetails[2];
                }
            }
        }

        static void checkOut(Booking[] rooms)
        {
            Console.WriteLine("Select a room to checkout:");
            for (int i = 0; i < 20; i++)
            {
                if (rooms[i].roomBooked == true)
                {
                    Console.WriteLine("Room: " + rooms[i].roomNumber);
                    Console.WriteLine("Customer first name: " + rooms[i].customerFirstName);
                    Console.WriteLine("Customer last name: " + rooms[i].customerLastName);
                }
            }
            int roomNum = Convert.ToInt32(Console.ReadLine());

            rooms[roomNum - 1].roomBooked = false;
        }

        static void saveCheckins(Booking[] rooms)
        {
            string fileName = @"C:\Temp\hotelBookings.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            { 
                
                for (int i = 0; i < 20; i++)
                {
                    if (rooms[i].roomBooked == true)
                    {
                        writer.WriteLine(rooms[i].roomNumber + ";" + rooms[i].customerFirstName + ";" + rooms[i].customerLastName);
                    }
                }
            }

        }


        static void Main(string[] args)
        {

            Booking[] rooms = new Booking[20];
            
            //Set all the rooms to not booked
            for (int i = 0; i < 20; i++)
            {
                rooms[i].roomBooked = false;
                rooms[i].roomNumber = i + 1;
            }


            int choice = mainMenu();

            do
            {

                switch(choice)
                {
                    case 1:
                        importCheckins(rooms);
                        break; 
                    case 2:
                        viewCheckins(rooms);
                        break;
                    case 3:
                        Checkin(rooms);
                        break;
                    case 4:
                        checkOut(rooms);
                        break;
                    case 5:
                        saveCheckins(rooms);
                        break;
                    case 6:
                        break;
                    case 7:
                        mainMenu();
                        break;
                    default:
                        Console.WriteLine("Please select a valid option!");
                        break;
                }

                choice = mainMenu();

            } while (choice != 6);

            Console.WriteLine("Thank you, have a nice day!");
        }
    }
}
