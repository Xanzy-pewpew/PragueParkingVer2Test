using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PragueParkingVer2Test
{
    public  class Folder
    {
        public void SaveToFolder()
        {
            var buildString = new StringBuilder();
            foreach (var vehicle in parkingSpot.Vehicles) { buildString.AppendLine($"{vehicle.Type}\n{vehicle.RegistrationNumber}\n{vehicle.ParkedTime}\n{vehicle.Spot}"); }
            File.WriteAllText("ParkingLot.txt", buildString.ToString());
        }
        public void Read()
        {
            var input = File.ReadAllLines("ParkingLot.txt");
            parkingSpot.Vehicles.Clear();
            for (int i = 0; i < input.Length; i++) { parkingSpot.RegVehicle(input[i], input[i + 1], int.Parse(input[i + 3]), DateTime.Parse(input[i + 2])); }
        }

        private readonly ParkingSpot parkingSpot;
        public Folder(ParkingSpot parkingSpot) { this.parkingSpot = parkingSpot; }
    }
}
