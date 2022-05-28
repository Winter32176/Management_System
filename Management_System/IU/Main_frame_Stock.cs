using Backend.Stock;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Management_System.IU
{
    public partial class Main_frame : Form
    {
        private void stock_Click(object sender, EventArgs e)
        {
            // "opens" stock panel
            vehiclePanel.Visible = false;
            orderPanel.Visible = false;
            stockPanel.Visible = true;

            // setting panel to proper size and possition
            stockPanel.Location = new Point(0, 130);
            stockPanel.Size = new System.Drawing.Size(1483, 667);

        }

        private void stockMenuAddB_Click(object sender, EventArgs e)
        {
            // "opens" add panel
            stockAddPanel.Visible = true;
            stockChangePanel.Visible = false;
            stockDeletePanel.Visible = false;
            stockSearchSortPanel.Visible = false;
            stockDisplayPanel.Visible = false;

            // setting panel to proper size and possition
            stockAddPanel.Location = new Point(230, 0);
            stockAddPanel.Size = new System.Drawing.Size(1272, 673);
        }
        private void stockAddB_Click(object sender, EventArgs e)
        {
            var iName = stockItemNameTB.Text.Replace("Name: ", "");
            var iQ = int.Parse(stockItemQTB.Text.Replace("Quantity: ", ""));
            var iWeight = float.Parse(stockItemWeightTB.Text.Replace("Weight: ", ""));
            var iPrCost = float.Parse(stockItemPrCostsTB.Text.Replace("Production costs: ", ""));
            var iSellpr = float.Parse(stockItemSellPrTB.Text.Replace("Selling price: ", ""));

            stockSystem.addItem(iName, (ItemType)stockItemTypeCB.SelectedIndex, iWeight, iQ, iPrCost, iSellpr);
        }


        private void stockMenuChangeB_Click(object sender, EventArgs e)
        {
            // "opens" change panel
            stockAddPanel.Visible = false;
            stockChangePanel.Visible = true;
            stockDeletePanel.Visible = false;
            stockSearchSortPanel.Visible = false;
            stockDisplayPanel.Visible = false;

            // setting panel to proper size and possition
            stockChangePanel.Location = new Point(230, 0);
            stockChangePanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            stockChangeData.DataSource = null;
            stockChangeData.DataSource = stockSystem.ItemsList;
        }

        private void stockChangeB_Click(object sender, EventArgs e)
        {
            var iID = int.Parse(stockChangeIdTB.Text.Replace("Item ID: ", ""));
            var iName = stockChangeNameTB.Text.Replace("Name: ", "");
            var iQ = int.Parse(stockChangeQTB.Text.Replace("Quantity: ", ""));
            var iWeight = float.Parse(stockChangeWeightTB.Text.Replace("Weight: ", ""));
            var iPrCost = float.Parse(stockChangePrCostsTB.Text.Replace("Production costs: ", ""));
            var iSellpr = float.Parse(stockChangeSellPrTB.Text.Replace("Selling price: ", ""));

            stockSystem.changeItem(iID, iName, (ItemType)stockChangeTypeCB.SelectedIndex, iWeight, iQ, iPrCost, iSellpr);

            // updating DataGrid
            stockChangeData.DataSource = null;
            stockChangeData.DataSource = stockSystem.ItemsList;
        }

        private void stockMenuDeleteB_Click(object sender, EventArgs e)
        {
            // "opens" delete panel
            stockAddPanel.Visible = false;
            stockChangePanel.Visible = false;
            stockDeletePanel.Visible = true;
            stockSearchSortPanel.Visible = false;
            stockDisplayPanel.Visible = false;

            // setting panel to proper size and possition
            stockDeletePanel.Location = new Point(230, 0);
            stockDeletePanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            stockDeleteData.DataSource = null;
            stockDeleteData.DataSource = stockSystem.ItemsList;
        }

        private void deleteStockItemB_Click(object sender, EventArgs e)
        {
            string itemID = deleteStockItemTB.Text.Replace("Item ID: ", "");
            if (itemID.Length > 0 && itemID.All(char.IsDigit))
                stockSystem.deleteItem(int.Parse(itemID));

            // updating DataGrid
            stockDeleteData.DataSource = null;
            stockDeleteData.DataSource = stockSystem.ItemsList;
        }


        private void stockMenuDisplayB_Click(object sender, EventArgs e)
        {
            // "opens" dsiplay panel
            stockAddPanel.Visible = false;
            stockChangePanel.Visible = false;
            stockDeletePanel.Visible = false;
            stockSearchSortPanel.Visible = false;
            stockDisplayPanel.Visible = true;

            // setting panel to proper size and possition
            stockDisplayPanel.Location = new Point(230, 0);
            stockDisplayPanel.Size = new System.Drawing.Size(1272, 673);
        }

        private void stockDisplayB_Click(object sender, EventArgs e)
        {
            // displaying only one item
            stockDisplayData.DataSource = null;
            stockDisplayData.DataSource = new StockSystem[] { stockSystem.searchItem(int.Parse(stockDisplayItemTB.Text.Replace("Item ID: ", ""))) };

        }

        private void stockDisplayAllB_Click(object sender, EventArgs e)
        {
            // updating DataGrid
            stockDisplayData.DataSource = null;
            stockDisplayData.DataSource = stockSystem.ItemsList;

            // resize cols to content fit !
            stockDisplayData.AutoResizeColumns();
            stockDisplayData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void stockDisplaySortCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update DataGrid with sorted items
            stockDisplayData.DataSource = null;
            stockDisplayData.DataSource = stockSystem.sortItems(stockDisplaySortCB.Text);
        }


        private void stockMenuSearchMoreB_Click(object sender, EventArgs e)
        {
            // "opens" search and more panel
            stockAddPanel.Visible = false;
            stockChangePanel.Visible = false;
            stockDeletePanel.Visible = false;
            stockSearchSortPanel.Visible = true;
            stockDisplayPanel.Visible = false;

            // setting panel to proper size and possition
            stockSearchSortPanel.Location = new Point(230, 0);
            stockSearchSortPanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            stockSFSumData.DataSource = null;
            stockSFSumData.DataSource = stockSystem.ItemsList;
        }

        private void stockPerformOpB_Click(object sender, EventArgs e)
        {
            stockSFSumData.DataSource = null;

            if (stockOpTypeCB.Text.Contains("Search"))
            {
                switch (stockCriteriaCB.Text)
                {
                    case "By name":
                        stockSFSumData.DataSource = stockSystem.searchItem(stockCriteriaTB.Text);
                        break;
                    case "By weight":
                        stockSFSumData.DataSource = stockSystem.searchItem(float.Parse(stockCriteriaTB.Text));
                        break;
                    default:
                        break;
                }

            }
            else
            {
                switch (stockCriteriaCB.Text)
                {
                    case "By weight":
                        stockSFSumData.DataSource = stockSystem.filterItems(float.Parse(stockCriteriaTB.Text));
                        break;
                    case "By quantity is more/equal than":
                        stockSFSumData.DataSource = stockSystem.filterItems(int.Parse(stockCriteriaTB.Text));
                        break;
                    default:
                        break;
                }
            }
        }

        // changing second combo box or performing operation depending on user choice 
        private void stockOpTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            stockCriteriaCB.Text = "Criteria";
            stockCriteriaCB.Items.Clear();
            switch (stockOpTypeCB.Text)
            {
                case "Search":
                    stockPerformOpB.Enabled = true;
                    stockCriteriaCB.Items.AddRange(new string[] { "By name", "By weight" });
                    break;
                case "Filter":
                    stockPerformOpB.Enabled = true;
                    stockCriteriaCB.Items.AddRange(new string[] { "By weight", "By quantity is more/equal than" });
                    break;
                case "Summary of production costs":
                    stockPerformOpB.Enabled = false;
                    MessageBox.Show("Sum: " + stockSystem.sumProdCosts().ToString());
                    break;
                case "Quantity of non chocolateItems":
                    stockPerformOpB.Enabled = false;
                    MessageBox.Show("Number: " + stockSystem.qNonChocolateItems().ToString());
                    break;
                default:
                    break;
            }
        }

    }
}
