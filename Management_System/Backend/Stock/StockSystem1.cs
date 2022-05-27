using System;
using System.Collections.Generic;

namespace Backend.Stock
{
    public partial class StockSystem
    {
        // all used System.Linq functins can be peformed by below code with some small changes.
        private bool findFunc(int itemID, ref int poss) // ref is used beouse is needed to return 2 values (one ref, one return). bool for indication of the success of the operation.
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                if (itemsList[i].itemID == itemID)
                {
                    poss = i;
                    return true;
                }
            }
            return false;
        }

        // subtracts the number of ordered items from the stock balance
        public int changeQuantity(int itemID, int quan)
        {
            int poss = itemsList.FindIndex(var => var.itemID == itemID);
            if (poss != -1) // poss is -1 if object is not found
            {
                if (itemsList[poss].itemQuantity >= quan) // checks that ordered quantity do not exceed stock balance
                {
                    itemsList[poss].itemQuantity -= quan;
                    return 0;
                }
                else
                {
                    return Math.Abs((quan - itemsList[poss].itemQuantity)); // returns the quantity by which the order exceeds the stock balance
                }
            }
            return -1;
        }

        // restore number of ordered of items if orders is deleted or not ordered.
        public void restoreQuantity(int itemID, int quan)
        {
            int poss = itemsList.FindIndex(var => var.itemID == itemID);
            itemsList[poss].itemQuantity +=quan;
        }

        public void addItem(string itemName, ItemType itemType, float itemWeight,
            int itemQuantity, float itemProdCost, float itemSellPrice)
        {
            itemsList.Add(new StockSystem(itemName, itemType, itemWeight,
             itemQuantity, itemProdCost, itemSellPrice));
        }

        public void deleteItem(int itemID)
        {
            int poss = itemsList.FindIndex(a => a.ItemID == itemID);
            if (poss != -1)
                itemsList.RemoveAt(poss);
        }

        public void changeItem(int itemID, string itemName, ItemType itemType, float itemWeight,
            int itemQuantity, float itemProdCost, float itemSellPrice)
        {
            int poss = itemsList.FindIndex(a => a.ItemID == itemID);
            if (poss != -1)
            {
                itemsList[poss] = new StockSystem(itemName, itemType, itemWeight, itemQuantity, itemProdCost, itemSellPrice);
                itemsList[poss].itemID = itemID;
                itemCounter--;
            }
        }
    }
}
