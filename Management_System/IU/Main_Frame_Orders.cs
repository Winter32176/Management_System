using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace Management_System.IU
{
    public partial class Main_frame : Form
    {
        private List<Tuple<int, int>> itemsToOrder = new List<Tuple<int, int>>(); // temporary store ordered items

        private void Order_Click(object sender, EventArgs e)
        {
            // "opens" order panel
            stockPanel.Visible = false;
            vehiclePanel.Visible = false;
            orderPanel.Visible = true;

            // setting panel to proper size and possition
            orderPanel.Location = new Point(0, 100);
            orderPanel.Size = new System.Drawing.Size(1483, 667);
        }

        private void orderMenuBAdd_Click(object sender, EventArgs e)
        {
            // "opens" add panel
            orderDeletePanel.Visible = false;
            orderDisplayPanel.Visible = false;
            orderAddPanel.Visible = true;

            // setting panel to proper size and possition
            orderAddPanel.Location = new Point(170, 0);
            orderAddPanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            itemOrData.DataSource = null;
            itemOrData.DataSource = stockSystem.ItemsList;



        }

        // adding records about vehicles in combo box
        private void orderAddPanel_visibleChange(object sender, EventArgs e)
        {
            if (orderAddPanel.Visible==true)
            {
                // adding records about vehicles in combo box
                vehiclesCB.Items.Clear();
                var vSize = vehicles.VechiclesList.Count;
                string[] vArray = new string[vSize];

                for (int i = 0; i < vSize; i++)
                {
                    vArray[i] = vehicles.VechiclesList[i].VechicleType.ToString() +
                        " ID:" + vehicles.VechiclesList[i].VehicleID.ToString() +
                        " LoadCap:" + vehicles.VechiclesList[i].LoadCap.ToString();
                }

                vehiclesCB.Items.AddRange(vArray);
            }
        }

        private void itemToOrderB_Click(object sender, EventArgs e)
        {
            string getID = itemIDTB.Text.Replace("Item ID: ", "");
            string getQuantity = quantityTB.Text.Replace("Quantity: ", "");
            if (getID.Length > 0 && getQuantity.Length > 0 && getID.All(char.IsDigit) && getQuantity.All(char.IsDigit)) // checks that fields are not empty and contain only digits
            {
                var itemID = int.Parse(getID);
                var quantity = int.Parse(getQuantity);
                var tryQuantity = stockSystem.changeQuantity(itemID, quantity);

                if (tryQuantity == 0)
                    itemsToOrder.Add(new Tuple<int, int>(itemID, quantity));
                else if (tryQuantity > 0)
                    MessageBox.Show("Quantity is exeeded by " + tryQuantity);
                else
                    MessageBox.Show("Item not found");
            }
            else
                MessageBox.Show("Typo");

            // updating DataGrid
            itemOrData.DataSource = null;
            itemOrData.DataSource = stockSystem.ItemsList;
        }

        private void placeOrderB_Click(object sender, EventArgs e)
        {
            // checks that order weight don't exceed vehicle max load
            if (orders.checkOrderWeight(itemsToOrder, vehiclesCB.SelectedIndex))
            {
                orders.addOrder(itemsToOrder, adressTB.Text.Replace("Adress: ", ""), vehiclesCB.SelectedIndex, deliveryDateTB.Text.Replace("Delivery date: ", ""));
                itemsToOrder.Clear();
                MessageBox.Show("Delivery:\n" + (orders.OrdersList.Last()).DeliveryObj.ToString()); // can be used insted [(orders.OrdersList.Count-1)]
            }
            else
            {
                foreach (Tuple<int, int> a in itemsToOrder)
                    stockSystem.restoreQuantity(a.Item1, a.Item2);

                itemsToOrder.Clear();

                itemOrData.DataSource = null;
                itemOrData.DataSource = stockSystem.ItemsList;

                MessageBox.Show("Order weight is bigger than the vehicle load capacity. Reorder again");
            }
        }

        private void orderMenuBDisplay_Click(object sender, EventArgs e)
        {
            // "opens" display panel
            orderDeletePanel.Visible = false;
            orderAddPanel.Visible = false;
            orderDisplayPanel.Visible = true;

            // setting panel to proper size and possition
            orderDisplayPanel.Location = new Point(170, 0);
            orderDisplayPanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            displayOrdersData.DataSource = null;
            displayOrdersData.DataSource = fillOrdersGrid();

            // resize cols to content fit !
            displayOrdersData.AutoResizeColumns();
            displayOrdersData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void orderMenuBDelete_Click(object sender, EventArgs e)
        {
            // "opens" delete panel
            orderDisplayPanel.Visible = false;
            orderAddPanel.Visible = false;
            orderDeletePanel.Visible = true;

            // setting panel to proper size and possition
            orderDeletePanel.Location = new Point(170, 0);
            orderDeletePanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            ordersDeleteDataGrid.DataSource = null;
            ordersDeleteDataGrid.DataSource = fillOrdersGrid();
        }

        private void deleteOrderB_Click(object sender, EventArgs e)
        {
            string orderID = deleteOrderTB.Text.Replace("Order ID: ", "");
            if (orderID.Length > 0 && orderID.All(char.IsDigit))
                orders.deleteOrder(int.Parse(orderID));

            // updates DataGrid
            ordersDeleteDataGrid.DataSource = null;
            ordersDeleteDataGrid.DataSource = fillOrdersGrid();
        }

        private DataTable fillOrdersGrid()
        {
            //dataGridView.DataSource = orders.OrdersList; // don't work properly
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Order ID", typeof(string)),
                new DataColumn("Ordered items", typeof(string)),
                new DataColumn("Order weight", typeof(string)),
                new DataColumn("Delivery", typeof(string)),
            });
            var list = orders.OrdersList;
            for (int i = 0; i < list.Count; i++)
            {
                var listOfItems = list[i].OrderedItems;
                string[] itemArray = { list[i].OrderID.ToString(), string.Join("; ", listOfItems), list[i].OrderWeight.ToString(), list[i].DeliveryObj.ToString() };
                dt.Rows.Add(itemArray);
            }

            return dt;
        }
    }
}
