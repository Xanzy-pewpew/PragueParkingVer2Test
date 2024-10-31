using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PragueParkingVer2Test.Vehicle;

namespace PragueParkingVer2Test
{
    public class ParkingSpot
    {
        public List<Vehicle> Vehicles { get; private set; }
        public ParkingSpot() { Vehicles = new List<Vehicle>(); }

        public Vehicle FindRegNum( string regNum) { return Vehicles.Find(vehicle => vehicle.RegistrationNumber == regNum); }
        public Vehicle RegVehicle(string type, string regNum, int spot, DateTime parkedStart)
        {
            var vehicle = new Vehicle(type, regNum, spot, parkedStart);
            Vehicles.Add(vehicle);
            return vehicle;
        }
        
        public bool DuplicateReg(string regNum)
        {
            return Vehicles.Exists(vehicle => vehicle.RegistrationNumber.Equals(regNum, StringComparison.OrdinalIgnoreCase));
        }

        //remove vehicle
        public void RemoveVehicle(Vehicle vehicle) { Vehicles.Remove(vehicle); }
        public int CountStatusSpot(int parkSpot) { return Vehicles.Count(vehicle => vehicle.Spot == parkSpot); }
        public bool SpotStatus(int parkSpot, string type)
        {
            int statusCount = CountStatusSpot(parkSpot);
            if (statusCount > 0) return false;
            if(type == "mc" && statusCount < 2) return false;
            return true;
        }
        public double CollectFee(Vehicle vehicle) { RemoveVehicle(vehicle); return CalFeeCost(vehicle); }
        public double CalFeeCost(Vehicle vehicle)
        {
            TimeSpan timeParked = DateTime.Now - Convert.ToDateTime(vehicle.ParkedTime);
            double afterTimeParked = timeParked.TotalMinutes;
            double totalfee = 0;
            //double parkedMinutes = timeParked.TotalMinutes;  //kanske inte behövs
            if (afterTimeParked > 10 && afterTimeParked < 120)
            {
                totalfee = (vehicle.Type == "mc") ? (int)VehiclePrice.MC * 2 : (int)VehiclePrice.Car * 2;
            }
            else if (timeParked.TotalMinutes >= 120)
            {
                double parkedHours = Math.Ceiling(afterTimeParked / 60);
                totalfee = (vehicle.Type == "mc") ? parkedHours * (int)VehiclePrice.MC : parkedHours * (int)VehiclePrice.Car;
            }
            return totalfee;
        }
    }
}
