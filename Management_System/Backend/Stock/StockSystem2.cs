using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Stock
{
    public partial class StockSystem : IComparable<StockSystem>
    {

        // search by name
        // list is returned becouse may be not one item
        public List<StockSystem> searchItem(string itemName)
        {
            List<StockSystem> searchList = new List<StockSystem>();
            searchList = itemsList.FindAll(var => var.ItemName.ToLower() == itemName.ToLower()); // case insensitive
            return searchList; // if not found return empty list
        }
        // search by id
        public StockSystem searchItem(int itemID)
        {
            var searchList = itemsList.Find(var => var.ItemID == itemID);
            return searchList; // if not found return empty object
        }
        // search by item weight
        public List<StockSystem> searchItem(float itemWeight)
        {
            List<StockSystem> searchList = new List<StockSystem>();
            searchList = itemsList.FindAll(var => var.ItemWeight == itemWeight);
            return searchList; // if not found return empty list
        }

        // filter by item weight
        public List<StockSystem> filterItems(float byWeight)
        {
            List<StockSystem> listTofilter = itemsList;
            return listTofilter.Where(var => var.ItemWeight == byWeight).ToList();
        }

        // filter by the number of products that have a greater or equal quantity to the specified
        public List<StockSystem> filterItems(int byQuantity)
        {
            return itemsList.Where(var => var.ItemQuantity >= byQuantity).ToList(); //// !!!! >=
        }

        // sums all production cost of items
        public float sumProdCosts()
        {
            float sum = 0;
            foreach (StockSystem a in itemsList)
                sum += a.ItemProdCost;
            return sum;
        }

        // counts how many non-chocolate items
        public int qNonChocolateItems()
        {
            int quantity = itemsList.Where(var => var.ItemType == ItemType.NonChocolate).Count();
            return quantity;
        }


        //sorting
        public List<StockSystem> sortItems(string sortTypeOrder)
        {
            List<StockSystem> sortedList = ItemsList;

            //sorting ascending order
            if (sortTypeOrder.Contains("By name"))
                sortedList.Sort((StockSystem x, StockSystem y) =>
                   x.ItemName.CompareTo(y.ItemName));
            else if(sortTypeOrder.Contains("By quantity"))
                sortedList.Sort((StockSystem x, StockSystem y) =>
                   x.ItemQuantity.CompareTo(y.ItemQuantity));

            // Descending order
            if (sortTypeOrder.Contains("descending"))
                sortedList.Reverse();

            return sortedList;
        }
        public int CompareTo(StockSystem itemToComp)
        {
            return this.itemName.CompareTo(itemToComp.itemName);
        }

    }
}
