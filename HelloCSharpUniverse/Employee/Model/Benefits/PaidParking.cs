using System;
using System.Collections.Generic;
using System.Text;

namespace Employees_DecoratorDP.Model.Benefits
{
    public class PaidParking : IBenefit
    {
        public string ParkingID { get; }

        public PaidParking(string parkingID)
            => ParkingID = parkingID;

        public string GetPrintableDetails()
            => $"\tPaid Parking personal ID: {ParkingID}";
    }
}