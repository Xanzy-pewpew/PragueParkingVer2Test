using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PragueParkingVer2Test
{
    class Program
    {
        static void Main(string[] args) { var program = new Program(); program.Run(); }
        private ParkingGarage parkingGarage;
        public Program()
        {
            var parkingSpot = new ParkingSpot();
            var folder = new Folder(parkingSpot);
            parkingGarage = new ParkingGarage(parkingSpot, folder);
        }
        private void Run() { parkingGarage.MainMenu(); }
    }
}