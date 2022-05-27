using System;


namespace Backend.Vehicle
{
    public class Train : VehicleD
    {
        private String trainNO;
        private String fuelType;
        private float trackGauge;

        public Train()
        {
        }

        public Train(string trainNO, string fuelType, float trackGauge, float loadCap, float fuelConsumption, VechicleType vechicleType)
        {
            this.VehicleID = ++VehicleCounter;
            this.trainNO = trainNO;
            this.fuelType = fuelType;
            this.trackGauge = trackGauge;
            this.LoadCap = loadCap;
            this.FuelConsumption = fuelConsumption;
            this.VechicleType = vechicleType;
        }

        public string TrainNO { get => trainNO; set => trainNO = value; }
        public string FuelType { get => fuelType; set => fuelType = value; }
        public float TrackGauge { get => trackGauge; set => trackGauge = value; }

        public override string ToString()
        {
            return "ID: " + VehicleID + " Train No: " + TrainNO + " Fuel type: " + FuelType
                + " Track gauge: " + TrackGauge + " mm Load capacity: " + LoadCap + "t Fuel consumption: " + FuelConsumption + "l ";
        }
    }
}
