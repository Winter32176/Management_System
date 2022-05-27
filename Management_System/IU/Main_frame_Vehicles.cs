using Backend.Vehicle;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Management_System.IU
{
    public partial class Main_frame : Form
    {
        private void vehicle_Click(object sender, EventArgs e)
        {
            // "opens" vehicle panel
            orderPanel.Visible = false;
            stockPanel.Visible = false;
            vehiclePanel.Visible = true;

            // setting panel to proper size and possition
            vehiclePanel.Location = new Point(0, 130);
            vehiclePanel.Size = new System.Drawing.Size(1483, 667);

        }

        private void vehicleMenuAddB_Click(object sender, EventArgs e)
        {
            // "opens" add panel
            vehicleDeletePanel.Visible = false;
            vehicleChangePanel.Visible = false;
            vehiclesDisplayPanel.Visible = false;
            vehicleAddPanel.Visible = true;

            // setting panel to proper size and possition
            vehicleAddPanel.Location = new Point(210, 0);
            vehicleAddPanel.Size = new System.Drawing.Size(1272, 673);
        }

        private void addVehicleB_Click(object sender, EventArgs e)
        {
            var vNO = vehicleNoTB.Text.Replace("Truck/Train No: ", "");
            var vRF = vehicleRefFTB.Text.Replace("Refrigerator/Fuel type: ", "");
            var vLoad = float.Parse(vehicleLoadTB.Text.Replace("Load capacity: ", ""));
            var vFuelC = float.Parse(vehicleFuelCTB.Text.Replace("Fuel consumption: ", ""));



            if (vehicleTypeCB.Text.Contains("Truck"))
            {
                vehicles.addVehicle(vNO, bool.Parse(vRF), vLoad, vFuelC);
            }
            else if (vehicleTypeCB.Text.Contains("Train"))
            {
                var vTrack = float.Parse(vehicleTrackTB.Text.Replace("Train track gauge: ", ""));
                vehicles.addVehicle(vNO, vRF, vTrack, vLoad, vFuelC);
                vehicleChangeTrackTB.Visible = false;
            }
        }

        // adding a new field depends on user input(diferent combo box)
        private void vehicleTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicleTypeCB.Text.Contains("Train"))
            {
                vehicleTrackTB.Visible = true;
            }
            else
            {
                vehicleChangeTrackTB.Visible = false;
            }
        }


        private void vehicleMenuChangeB_Click(object sender, EventArgs e)
        {
            // "opens" change panel
            vehicleDeletePanel.Visible = false;
            vehicleChangePanel.Visible = true;
            vehiclesDisplayPanel.Visible = false;
            vehicleAddPanel.Visible = false;

            // setting panel to proper size and possition
            vehicleChangePanel.Location = new Point(210, 0);
            vehicleChangePanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            vehicleChangeData.DataSource = null;
            vehicleChangeData.DataSource = fillVehiclesGrid();

            // resize cols to content fit !
            vehicleChangeData.AutoResizeColumns();
            vehicleChangeData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void changeVehicleB_Click(object sender, EventArgs e)
        {
            var vID = int.Parse(vehicleChangeIdTB.Text.Replace("Truck/Train ID: ", ""));
            var vNO = vehicleChangeNoTB.Text.Replace("Truck/Train No: ", "");
            var vRF = vehicleChangeRefFTB.Text.Replace("Refrigerator/Fuel type: ", "");
            var vLoad = float.Parse(vehicleChangeLoadTB.Text.Replace("Load capacity: ", ""));
            var vFuelC = float.Parse(vehicleChangeFuelCTB.Text.Replace("Fuel consumption: ", ""));

            if (vehicleChangeTypeCB.Text.Contains("Truck"))
                vehicles.changeVehicle(vID, vNO, bool.Parse(vRF), vLoad, vFuelC);
            else if (vehicleChangeTypeCB.Text.Contains("Train"))
            {
                var vTrack = float.Parse(vehicleChangeTrackTB.Text.Replace("Train track gauge: ", ""));
                vehicles.changeVehicle(vID, vNO, vRF, vTrack, vLoad, vFuelC);
                vehicleChangeTrackTB.Visible = false;
            }

            // updating DataGrid
            vehicleChangeData.DataSource = null;
            vehicleChangeData.DataSource = fillVehiclesGrid();

            // resize cols to content fit !
            vehicleChangeData.AutoResizeColumns();
            vehicleChangeData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        // adding a new field depends on user input  (diferent combo box)
        private void vehicleChangeTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicleChangeTypeCB.Text.Contains("Train"))
            {
                vehicleChangeTrackTB.Visible = true;
            }
            else
            {
                vehicleChangeTrackTB.Visible = false;
            }
        }


        private void vehicleMenuDisplayB_Click(object sender, EventArgs e)
        {
            // "opens" display panel
            vehicleAddPanel.Visible = false;
            vehicleChangePanel.Visible = false;
            vehicleDeletePanel.Visible = false;
            vehiclesDisplayPanel.Visible = true;

            // setting panel to proper size and possition
            vehiclesDisplayPanel.Location = new Point(210, 0);
            vehiclesDisplayPanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            vehiclesDisplayData.DataSource = null;
            vehiclesDisplayData.DataSource = fillVehiclesGrid();

            // resize cols to content fit !
            vehiclesDisplayData.AutoResizeColumns();
            vehiclesDisplayData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }

        private void vehicleMenuDeleteB_Click(object sender, EventArgs e)
        {
            // "opens" delete panel
            vehicleAddPanel.Visible = false;
            vehicleChangePanel.Visible = false;
            vehiclesDisplayPanel.Visible = false;
            vehicleDeletePanel.Visible = true;

            // setting panel to proper size and possition
            vehicleDeletePanel.Location = new Point(210, 0);
            vehicleDeletePanel.Size = new System.Drawing.Size(1272, 673);

            // updating DataGrid
            vehicleDeleteDataGrid.DataSource = null;
            vehicleDeleteDataGrid.DataSource = fillVehiclesGrid();

            // resize cols to content fit !
            vehicleDeleteDataGrid.AutoResizeColumns();
            vehicleDeleteDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void vehicleDeleteB_Click(object sender, EventArgs e)
        {
            string vehicleID = deleteVehicleTB.Text.Replace("Vehicle ID: ", "");
            if (vehicleID.Length > 0 && vehicleID.All(char.IsDigit))
                vehicles.deleteVehicle(int.Parse(vehicleID));

            // updating DataGrid
            vehicleDeleteDataGrid.DataSource = null;
            vehicleDeleteDataGrid.DataSource = fillVehiclesGrid();
        }

        private DataTable fillVehiclesGrid()
        {
            //dataGridView.DataSource = vehicles.VechiclesList; // don't work properly
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Vehicle ID", typeof(string)),
                new DataColumn("vehiclesType", typeof(string)),
                new DataColumn("Vehicle", typeof(string)),
                new DataColumn("loadCap", typeof(string)),
                new DataColumn("fuelConsumption", typeof(string)),

            });

            foreach (VehicleD a in vehicles.VechiclesList)
            {
                string[] vArr = { a.VehicleID.ToString(), a.VechicleType.ToString(), a.ToString(), a.LoadCap.ToString(), a.FuelConsumption.ToString() };
                dt.Rows.Add(vArr);
            }

            return dt;

        }





    }
}
