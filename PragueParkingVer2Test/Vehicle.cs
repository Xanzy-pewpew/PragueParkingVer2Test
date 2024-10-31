using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingVer2Test
{
    public class Vehicle
    {
        public enum VehicleType { MC, Car }
        public Vehicle(string type, string regNum, int spot, DateTime parkedTimeStarted)
        {
            Type = type;
            RegistrationNumber = regNum;
            Spot = spot;
            ParkedTime = parkedTimeStarted;
        }
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public int Spot { get; set; }
        public DateTime ParkedTime { get; set; }

        public string GetParkedDate() { string spd = ParkedTime.ToString("dd:MM:yyyy"); return spd; }
        public string GetParkedTime() { string spt = ParkedTime.ToString("HH:mm:ss"); return spt; }
    }
}
