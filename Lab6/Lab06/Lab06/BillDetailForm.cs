using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab06
{
    public partial class BillDetailForm : Form
    {
        int ID;
        public BillDetailForm()
        {
            InitializeComponent();
        }

        public void LoadBillDetail(int id)
        {
            this.ID = id;

            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT Name FROM Bills WHERE ID = " + id;
            sqlConnection.Open();
            string billName = sqlCommand.ExecuteScalar().ToString();
            this.Text = billName +" "+ id;

            string query = string.Format(
                " SELECT Name, Unit, Price, Quantity, Price * Quantity as Total FROM BillDetails" +
                " JOIN Food ON BillDetails.FoodID = Food.ID" +
                " WHERE BillDetails.InvoiceID = {0}",id);
            sqlCommand.CommandText = query;

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable("Food");
            adapter.Fill(dt);

            dgvBillDetails.DataSource = dt;
            dgvBillDetails.Columns[0].ReadOnly = true;

            sqlConnection.Dispose();
            adapter.Dispose();
        }
    }
}
