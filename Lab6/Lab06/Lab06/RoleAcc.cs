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
    public partial class RoleAcc : Form
    {
        public RoleAcc()
        {
            InitializeComponent();
        }
        public void LoadRole(string name)
        {
            string connectionString = "serverDESKTOP-EQOPDBI\\SQLEXPRESS database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT a.AccountName, r.RoleName " +
                " from Role r, RoleAccount ra, Account a" +
                " Where a.AccountName = ra.AccountName and r.ID = ra.RoleID and a.AccountName = '" + name +"'";

            sqlConnection.Open();

            this.Text = "Danh sách role của tài khoản "+name;

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            DataTable table = new DataTable("Role");
            adapter.Fill(table);

            dgvRole.DataSource = table;

            // Prevent user to edit ID
            dgvRole.Columns[0].ReadOnly = true;

            sqlConnection.Close();
        }
    }
}
