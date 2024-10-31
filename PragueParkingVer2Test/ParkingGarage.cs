using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingVer2Test
{
    public class ParkingGarage
    {
        private readonly ParkingSpot _parkingSpot;
        private readonly Folder _folder;
        public ParkingGarage(ParkingSpot parkingSpot, Folder folder)
        {
            _parkingSpot = parkingSpot;
            _folder = folder;
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                DisplayHeader();
                DisplayOption();
                switch (Console.ReadLine()?.ToLower())
                {
                    case "1": AddVehicle(); break;
                    case "2": RemoveVehicle(); break;
                    case "3": SearchVehicle(); break;
                    case "4": ViewVehicleList(); break;
                    case "5": ReadFolder(); break;
                    case "6": SaveToFolder(); break;
                    case "0": break;
                    default: break;
                }
            }
        }
        private void DisplayHeader()
        {
            Console.WriteLine($"-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-"
                + "\n-%-%- Welcome to our Luxury Garage 2.0. -%-%-"
                + $"\n-%-%-   {DateTime.Now},{DateTime.Today.DayOfWeek}    -%-%-"
                + "\n-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-%-\n");
        }
        private void DisplayOption()
        {
            Console.WriteLine("Please choose from the menu options"
                + "\n(1): Add New Customer"
                + "\n(2): Remove or Move Vehicle"
                + "\n(3): Search Vehicle"
                + "\n(4): View Parked Vehicles"
                + "\n(5): Folder // Out of order"
                + "\n(6): Save to Folder"
                + "\n(0): Exit Program");
        }
        public void AddVehicle()
        {
            string type = GetInput("Please enter the type of vehicle (Car or MC): ", v => v == "car" || v == "mc");
            string regNumber = GetInput("Enter the registration number (max 10 characters): ", v => !string.IsNullOrEmpty(v) && v.Length == 10 && !_parkingSpot.DuplicateReg(v));
            int parkSpot = GetParkingSpot(type);
            if (parkSpot != -1)
            {
                _parkingSpot.RegVehicle(type, regNumber, parkSpot, DateTime.Now);
                Console.Write($"Vehicle: {type}, with registration number {regNumber} has been parked at spot nr: {parkSpot}.");
            }    
        }
        public void RemoveVehicle() => SearchVehicle(); //tar bort fordon
        public void SearchVehicle()
        {
            string regNum = GetInput("Please Enter the registration number: ", v => !string.IsNullOrEmpty(v));
            Vehicle vehicle = _parkingSpot.FindRegNum(regNum);
            if (vehicle != null)
            {
                GetFindVehicle(vehicle, _parkingSpot.CalFeeCost(vehicle));
            }
            else
            {
                Console.WriteLine($"Vehicle with registration number {regNum} does not exist in the system.");
            }
        }
        public void ViewVehicleList()
        {
            foreach (var vehicle in _parkingSpot.Vehicles.OrderBy(v => v.Spot))
                Console.Write($"Parking spot nr: {vehicle.Spot}, Type:{vehicle.Type}, Registration Number:{vehicle.RegistrationNumber}, Parked at:{vehicle.ParkedTime}");
            Console.Write("Press enter to continue...");
            Console.ReadKey();
        }
        public void MoveCurrentVehicle(Vehicle vehicle) //Flytta bilen
        {
            int newParkSpot = GetParkingSpot(vehicle.Type);
            if (newParkSpot != -1)
            {
                _parkingSpot.RemoveVehicle(vehicle);
                _parkingSpot.RegVehicle(vehicle.Type, vehicle.RegistrationNumber, newParkSpot, vehicle.ParkedTime);
                Console.Write($"{vehicle.Type} with registration number {vehicle.RegistrationNumber} has been moved to parking spot {newParkSpot}. "
                +"\nPress enter to continue...");
                Console.ReadKey();
            }
        }
        public void ReadFolder() { Console.Write("Folder Loading..."); _folder.Read(); }
        public void SaveToFolder() { Console.Write("Saving to folder..."); _folder.SaveToFolder(); Console.Write("Press Enter to continue..."); }
        public void GetRemoveVehicle(Vehicle vehicle)
        {
            double fee = _parkingSpot.CollectFee(vehicle);
            Console.Write($"Vehicle: {vehicle.Type} has been removed from parking spot {vehicle.Type}. Total price is {fee} CZK. "
            +"\nPress enter to continue...");
            Console.ReadKey();
        }
        public void GetFindVehicle(Vehicle vehicle, double currentFee)
        {
            while (true)
            {
                Console.WriteLine($"Vehicle {vehicle.Type} is parked at spot nr: {vehicle.Spot},"
                    + $"parked since {vehicle.ParkedTime:g}. Current fee is {currentFee} CZK."
                    + $"\n1 - Remove\n2 - Move Vehicle\n3 - Return to Main Menu");

                switch (Console.ReadLine()?.ToLower())
                {
                    case "1": GetRemoveVehicle(vehicle); return;  //Anropar metod remove vehicle
                    case "2": MoveCurrentVehicle(vehicle); return; //Anropar metod move vehicle
                    case "3": return;
                    default: Console.WriteLine("Invalid input"); break;
                }
            }
        }
        public string GetInput(string prompt, Func<string, bool> validation)
        {
            while (true)
            {
                Console.Clear();
                Console.Write(prompt);
                string? input = Console.ReadLine()?.ToLower();
                if (validation(input)) return input;
                Console.Write("Invalid Input. Please try again.");
            }
        }
        public int GetParkingSpot(string type) =>
            int.TryParse(GetInput($"Pick a parking spot for your {type} choose between 1-100: ",
                v => int.TryParse(v, out int spot) && spot > 0 && spot <= 100 && !_parkingSpot.SpotStatus(spot, type)), out int spot) ? spot : -1; //funkar välja spot när man väljer bil
    }
}
//behöver fixas:välja spot när man väljer car, folder classen, view parked vehicle
