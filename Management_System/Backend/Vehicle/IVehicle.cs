using System;
using System.Collections.Generic;

namespace Backend.Vehicle
{
    public interface IVehicle
    {
        VehicleD getVehicle(int vehicleId);
        void addVehicle(string trainNO, string fuelType, float trackGauge, float loadCap, float fuelConsumption);
        void addVehicle(string truckNo, bool refrigerator, float loadCap, float fuelConsumption);
        void changeVehicle(int vehicleID, string truckNo, bool refrigerator, float loadCap, float fuelConsumption);
        void changeVehicle(int vehicleID, string trainNO, string fuelType, float trackGauge, float loadCap, float fuelConsumption);
        void deleteVehicle(int vehicleId);
    }
}
