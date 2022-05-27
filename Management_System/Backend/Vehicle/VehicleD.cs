using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;


namespace Backend.Vehicle
{
    // is needed to truck and train saved and restored correctly from/in file
    [Newtonsoft.Json.JsonConverter(typeof(JsonSubtypes), "vechicleType")]
    [JsonSubtypes.KnownSubType(typeof(Truck), 1)]
    [JsonSubtypes.KnownSubType(typeof(Train), 2)]
    public class VehicleD : IVehicle
    {
        private static List<VehicleD> vehiclesList = new List<VehicleD>(); // main list to keep vehicles while app is working

        private static int vehicleCounter = 0; // global counter, to increment id

        private int vehicleID;
        private float loadCap;
        private float fuelConsumption;
        private VechicleType vechicleType;

        public int VehicleID { get { return this.vehicleID; } set => vehicleID = value; }
        public float FuelConsumption { get { return this.fuelConsumption; } set => fuelConsumption = value; }
        public float LoadCap { get { return this.loadCap; } set => loadCap = value; }
        protected static int VehicleCounter { get => vehicleCounter; set => vehicleCounter = value; }
        public VechicleType VechicleType { get { return this.vechicleType; } set => vechicleType = value; }

        // to avoid errors when saving and geting data
        [JsonIgnore]
        [IgnoreDataMember]
        public List<VehicleD> VechiclesList { get { return vehiclesList; } }

        public VehicleD()
        {
        }

        // saving data in json file
        public void vehiclesToFile()
        {
            var str = JsonConvert.SerializeObject(vehiclesList);
            File.WriteAllText("vehiclesList.json", str);
        }

        // getting data from json file
        public void getDataFromFile()
        {
            if (File.Exists("vehiclesList.json") && new FileInfo("vehiclesList.json").Length > 0)
            {
                string str = File.ReadAllText("vehiclesList.json"); // temp variable for info from file
                var list = JsonConvert.DeserializeObject<List<VehicleD>>(str);
                vehiclesList = list;

                // to get proper id's. When deletion of record hapens id's doesn't changes, and it may be that id are bigger than counter !!!!
                var lastPoss = vehiclesList.Count-1;
                vehicleCounter = vehiclesList[lastPoss].VehicleID;
            }
        }

        // two types of adding. For truck and train
        public void addVehicle(string trainNO, string fuelType, float trackGauge, float loadCap, float fuelConsumption)
        {
            vehiclesList.Add(new Train(trainNO, fuelType, trackGauge, loadCap, fuelConsumption, VechicleType.Train));
        }
        public void addVehicle(string truckNo, bool refrigerator, float loadCap, float fuelConsumption)
        {
            vehiclesList.Add(new Truck(truckNo, refrigerator, loadCap, fuelConsumption, VechicleType.Truck));
        }

        // two types of changing
        public void changeVehicle(int vehicleID, string truckNo, bool refrigerator, float loadCap, float fuelConsumption)
        {
            int poss = vehiclesList.FindIndex(a => a.VehicleID == vehicleID);
            vehiclesList[poss] = new Truck(truckNo, refrigerator, loadCap, fuelConsumption, VechicleType.Truck);
            vehiclesList[poss].vehicleID = vehicleID;
            vehicleCounter--;
        }
        public void changeVehicle(int vehicleID, string trainNO, string fuelType, float trackGauge, float loadCap, float fuelConsumption)
        {
            int poss = vehiclesList.FindIndex(a => a.VehicleID == vehicleID);
            vehiclesList[poss] = new Train(trainNO, fuelType, trackGauge, loadCap, fuelConsumption, VechicleType.Train);
            vehiclesList[poss].vehicleID = vehicleID;
            vehicleCounter--;
        }

        public VehicleD getVehicle(int vehicleID)
        {
            return vehiclesList.Find(a => a.VehicleID == vehicleID);
        }

        public void deleteVehicle(int vehicleID)
        {
            int poss = vehiclesList.FindIndex(a => a.VehicleID == vehicleID);
            if (poss != -1)
                vehiclesList.RemoveAt(poss);
        }

        public override string ToString()
        {
            return "ID " + VehicleID + " Load capacity: " + LoadCap + "t Fuel consumption" + FuelConsumption + "l ";
        }
    }




}
