using Backend.Orders;
using Backend.Stock;
using Backend.Vehicle;
using System;
using System.Windows.Forms;

namespace Management_System.IU
{
    public partial class Main_frame : Form
    {
        // prepering "clases for work" 
        public static VehicleD vehicles = new VehicleD();
        public static StockSystem stockSystem = new StockSystem();
        public static OrderD orders = new OrderD();

        public Main_frame()
        {
            InitializeComponent();
        }

        private void Main_frame_Load(object sender, EventArgs e)
        {
            // getting data from files when main form starts
            stockSystem.getDataFromFile();
            vehicles.getDataFromFile();
            orders.getDataFromFile();
        }

        private void saveExit_Click(object sender, EventArgs e)
        {
            // save data to file and exit of application
            stockSystem.stockSysytemToFile();
            vehicles.vehiclesToFile();
            orders.ordersToFile();
            Application.Exit();
        }

    }


}


