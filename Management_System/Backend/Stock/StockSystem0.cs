using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Backend.Stock
{
    public partial class StockSystem
    {
        private static List<StockSystem> itemsList = new List<StockSystem>(); // main list to keep items while app is working

        private static int itemCounter = 0; // global counter, to increment id

        private int itemID;
        private String itemName;
        private ItemType itemType;
        private float itemWeight;
        private int itemQuantity;
        private float itemProdCost;
        private float itemSellPrice;

        public int ItemID { get { return this.itemID; } set => itemID = value; }
        public String ItemName { get { return this.itemName; } set => itemName = value; }
        public ItemType ItemType { get { return this.itemType; } set => itemType = value; }
        public float ItemWeight { get { return this.itemWeight; } set => itemWeight = value; }
        public int ItemQuantity { get { return this.itemQuantity; } set => itemQuantity = value; }
        public float ItemProdCost { get { return this.itemProdCost; } set => itemProdCost = value; }
        public float ItemSellPrice { get { return this.itemSellPrice; } set => itemSellPrice = value; }

        // to avoid errors when saving and geting data
        [JsonIgnore]
        [IgnoreDataMember]
        public List<StockSystem> ItemsList { get { return itemsList; } }

        public StockSystem()
        {
        }

        public StockSystem(string itemName, ItemType itemType, float itemWeight,
            int itemQuantity, float itemProdCost, float itemSellPrice)
        {
            this.itemID = ++itemCounter;
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemWeight = itemWeight;
            this.itemQuantity = itemQuantity;
            this.itemProdCost = itemProdCost;
            this.itemSellPrice = itemSellPrice;
        }


        // saving data in json file
        public void stockSysytemToFile()
        {
            if (itemsList.Count > 0)
            {
                var str = JsonConvert.SerializeObject(itemsList);
                File.WriteAllText("StockSystem.json", str);
            }
        }

        // getting data from json file
        public void getDataFromFile()
        {
            if (File.Exists("StockSystem.json") && new FileInfo("StockSystem.json").Length > 2)// empty json file contains two chars -> []
            {
                string str = File.ReadAllText("StockSystem.json"); // temp variable for info from file
                var list = JsonConvert.DeserializeObject<List<StockSystem>>(str);
                itemsList = list;
                // to get proper id's. When deletion of record hapens id's doesn't changes, and it may be that id are bigger than counter !!!!
                var lastPoss = itemsList.Count - 1;
                itemCounter = itemsList[lastPoss].itemID;
            }
        }

        public override string ToString()
        {
            return "[" + " ID: " + itemID + " Name: " + itemName + " Type: " + itemType + " Weight: " + itemWeight
                + " Quantity: " + itemQuantity + " Production cost: " + itemProdCost + " Selling price: " + itemSellPrice + " Profit: " + " ]";
        }
    }
}
