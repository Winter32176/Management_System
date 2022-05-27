using Backend.Stock;
using Backend.Vehicle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Backend.Orders
{
    // 
    public class OrderedItem
    {
        private int itemID;
        private String itemName;
        private float itemWeight;
        private int orderedItemQuantity;

        public OrderedItem(int itemID, string itemName, float itemWeight, int orderedItemQuantity)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemWeight = itemWeight;
            this.orderedItemQuantity = orderedItemQuantity;
        }

        public int ItemID { get => itemID; set => itemID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public float ItemWeight { get => itemWeight; set => itemWeight = value; }
        public int OrderedItemQuantity { get => orderedItemQuantity; set => orderedItemQuantity = value; }

        public override string ToString()
        {
            return "[ ID: " + itemID + " Name: " + itemName + " Weight: " + itemWeight + " Quantity: " + orderedItemQuantity + "]";
        }
    }


    public class OrderD
    {
        private static List<OrderD> ordersList = new List<OrderD>(); // main list to keep orders while app is working

        private static int orderCounter = 0; // global counter, to increment id

        private int orderID;
        private List<OrderedItem> orderedItems = new List<OrderedItem>();
        private float orderWeight;
        private Delivery deliveryObj;

        // to avoid errors when saving and geting data
        [JsonIgnore]
        [IgnoreDataMember]
        public List<OrderD> OrdersList { get { return ordersList; } }

        public int OrderID { get { return orderID; } set => orderID = value; }
        public float OrderWeight { get { return orderWeight; } set => orderWeight = value; }
        public Delivery DeliveryObj { get { return deliveryObj; } set => deliveryObj = value; }
        public List<OrderedItem> OrderedItems { get { return this.orderedItems; } set => orderedItems = value; }

        public OrderD()
        {

        }

        private OrderD(List<Tuple<int, int>> itemsToOrder, Delivery deliveryObj)
        {
            this.orderID = ++orderCounter;
            foreach (Tuple<int, int> a in itemsToOrder)
            {
                addItemsInOrder(a.Item1, a.Item2);
            }
            this.orderWeight = calcOrderWeight();
            this.deliveryObj = deliveryObj;
        }


        // saving data in json file
        public void ordersToFile()
        {
            if (ordersList.Count > 0)
            {
                var str = JsonConvert.SerializeObject(ordersList);
                File.WriteAllText("ordersList.json", str);
            }
        }

        // getting data from json file
        public void getDataFromFile()
        {
            if (File.Exists("ordersList.json") && new FileInfo("ordersList.json").Length > 2)// empty json file contains two chars -> []
            {
                string str = File.ReadAllText("ordersList.json"); // temp variable for info from file
                var list = JsonConvert.DeserializeObject<List<OrderD>>(str);
                ordersList = list;
                // to get proper id's. When deletion of record hapens id's doesn't changes, and it may be that id are bigger than counter !!!!
                var lastPoss = ordersList.Count - 1;
                orderCounter = ordersList[lastPoss].orderID;
            }
        }

        // creating OrderedItem object to add them in order
        public void addItemsInOrder(int itemID, int orderedItemQuantity)
        {
            StockSystem stockSystem = new StockSystem();
            var item = stockSystem.searchItem(itemID);
            orderedItems.Add(new OrderedItem(itemID, item.ItemName, item.ItemWeight, orderedItemQuantity));

        }

        public void addOrder(List<Tuple<int, int>> itemsToOrder, string deliveryAddress, int vechiclePossInList, String deliveryDate)
        {
            VehicleD vehicles = new VehicleD();
            ordersList.Add(new OrderD(itemsToOrder, new Delivery(deliveryAddress, vehicles.VechiclesList[vechiclePossInList], deliveryDate)));
        }

        private float calcOrderWeight()
        {
            float totalWeight = 0;
            foreach (OrderedItem a in OrderedItems)
                totalWeight += (a.ItemWeight * a.OrderedItemQuantity);
            return totalWeight;
        }

        // check that total weight of order do not exceed vehicle load capacity
        public bool checkOrderWeight(List<Tuple<int, int>> itemsToOrder, int vechiclePossInList)
        {
            float totalWeight = 0;
            StockSystem stockSystem = new StockSystem();

            foreach (Tuple<int, int> a in itemsToOrder)
                totalWeight += ((stockSystem.searchItem(a.Item1)).ItemWeight * a.Item2);

            VehicleD vehicles = new VehicleD();
            if ((vehicles.VechiclesList[vechiclePossInList].LoadCap * 1000) >= totalWeight) // load in tons, weight in kg
                return true;
            else
                return false;
        }

        public void deleteOrder(int orderID)
        {
            int poss = OrdersList.FindIndex(a => a.orderID == orderID);
            if (poss != -1)
            {
                // restore item quantity which were ordered
                StockSystem stockSystem = new StockSystem();
                foreach (OrderedItem orderedItem in ordersList[poss].orderedItems)
                    stockSystem.restoreQuantity(orderedItem.ItemID, orderedItem.OrderedItemQuantity);

                ordersList.RemoveAt(poss);
            }

        }

        public override string ToString()
        {
            return "Order ID: " + OrderID + " Ordered items: " + string.Join(";", OrderedItems)
                + "Order weight: " + OrderWeight + " Delivery:" + DeliveryObj.ToString();
        }
    }
}
