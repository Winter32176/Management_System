using System;
using System.Globalization;
using Backend.Vehicle;

namespace Backend.Orders
{
    public class Delivery
    {
        private static int deliveryCounter = 0; // global counter, to increment id

        private int deliveryID;
        private String deliveryAddress;
        private VehicleD deliveryVechicle;
        private String deliveryDate;


        public Delivery()
        {
        }

        public Delivery(string deliveryAddress, VehicleD deliveryVechicle, String deliveryDate)
        {
            this.deliveryID = ++deliveryCounter;
            this.deliveryAddress = deliveryAddress;
            this.deliveryVechicle = deliveryVechicle;
            this.deliveryDate = deliveryDate;
        }

        public int DeliveryID { get => deliveryID; set => deliveryID = value; }
        public string DeliveryAddress { get => deliveryAddress; set => deliveryAddress = value; }
        public VehicleD DeliveryVechicle { get => deliveryVechicle; set => deliveryVechicle = value; }
        public String DeliveryDate { get => deliveryDate; set => deliveryDate = value; }

        private double calcDelivTime()
        {
            CultureInfo culture = new CultureInfo("it-IT"); // to avoid problems in parsing. Different computer have different default formats.
            DateTime parsedDate = DateTime.ParseExact(this.DeliveryDate, "d", culture);
            DateTime dateNow = DateTime.Today;
            return (parsedDate - dateNow).TotalDays;
        }

        public override string ToString()
        {
            return "Delivery_ID: " + DeliveryID + " Vehicle " + DeliveryVechicle.ToString()
                + " Adress: " + DeliveryAddress + " Date: " + DeliveryDate + " Days to delivery:" + calcDelivTime().ToString();
        }
    }
}

