using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParkingVer2Test
{
    public enum VehiclePrice
    {
        Car = 20, MC = 10
    }
    public class ParkingFeeCal
    {
        public int CalParkingFee(VehiclePrice vehicle)
        {
            return (int)vehicle;
        }
    }
}
