using System;


namespace Backend.Vehicle
{
    public class Truck : VehicleD
    {
        private String truckNo;
        private bool refrigerator;

        public Truck()
        {
        }

        public Truck(string truckNo, bool refrigerator, float loadCap, float fuelConsumption, VechicleType vechicleType)
        {
            this.VehicleID = ++VehicleCounter;
            this.VechicleType = vechicleType;
            this.truckNo = truckNo;
            this.refrigerator = refrigerator;
            this.LoadCap = loadCap;
            this.FuelConsumption = fuelConsumption;

        }

        public string TruckNo { get => truckNo; set => truckNo = value; }
        public bool Refrigerator { get => refrigerator; set => refrigerator = value; }

        public override string ToString()
        {
            return "ID: " + VehicleID + " Truck No: " + TruckNo + " Refrigirator: " + Refrigerator
                + " Load capacity: " + LoadCap + "t Fuel consumption: " + FuelConsumption + "l ";
        }
    }
}
