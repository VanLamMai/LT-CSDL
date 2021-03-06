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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dtpkFromTime.Format = DateTimePickerFormat.Custom;
            dtpkFromTime.CustomFormat = "dd-MM-yyyy";
            dtpkToTime.Format = DateTimePickerFormat.Custom;
            dtpkToTime.CustomFormat = "dd-MM-yyyy";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";

            SqlConnection sqlConnection = new SqlConnection(connectionString);


            string query = "SELECT ID, NAME, TYPE FROM Category";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = query;

            sqlConnection.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            DisplayCategory(sqlDataReader);

            sqlConnection.Close();
        }

        private void DisplayCategory(SqlDataReader rd)
        {
            lvCategory.Items.Clear();
            while (rd.Read())
            {
                ListViewItem item = new ListViewItem(rd["ID"].ToString());
                lvCategory.Items.Add(item);
                item.SubItems.Add(rd["Name"].ToString());
                item.SubItems.Add(rd["Type"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N'" + txtName.Text + "', " + txtType.Text + ")";

                sqlConnection.Open();

                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                if (numOfRowsEffected == 1)
                {
                    MessageBox.Show("Thêm món ăn thành công");
                    btnLoad.PerformClick();
                    txtName.Text = "";
                    txtType.Text = "";
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, Vui lòng thử lại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            }
        }

        private bool Validation()
        {
            if (txtName.Text.Equals("")) return false;
            else if (txtType.Text.Equals("")) return false;
            return true;
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvCategory.SelectedItems[0];

            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text == "0" ? "Thức uống" : "Đồ ăn";

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "UPDATE Category SET Name = N'" + txtName.Text + 
                                                    "', [Type] = " + (txtType.Text=="Thức uống"? 0:1) + 
                                                    " WHERE ID = " + txtID.Text;

            sqlConnection.Open();
            int numOfRowEffected = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            if(numOfRowEffected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                item.SubItems[1].Text = txtName.Text = txtName.Text;
                item.SubItems[2].Text = txtType.Text == "Thức uống"? "0":"1";

                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Cập nhật món ăn thành công");

            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, Vui lòng thử lại");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM Category " + "WHERE ID = " + txtID.Text;

                sqlConnection.Open();
                int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                if (numOfRowsEffected == 1)
                {
                    ListViewItem item = lvCategory.SelectedItems[0];
                    lvCategory.Items.Remove(item);

                    txtID.Text = "";
                    txtName.Text = "";
                    txtType.Text = "";

                    btnDelete.Enabled = false;
                    btnUpdate.Enabled = false;

                    MessageBox.Show("Xóa nhóm món ăn thành công");

                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, Vui lòng thử lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi SQL");
            }
            
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if(txtID.Text != "")
            {
                frmFood frm = new frmFood();
                frm.Show(this);
                frm.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            if(dtpkFromTime.Value.ToShortDateString() != null || dtpkToTime.Value.ToShortDateString()!= null)
            {
                BillForm frm = new BillForm();
                frm.Show(this);
                frm.LoadBills(dtpkFromTime.Value.ToString(), dtpkToTime.Value.ToString());
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            AccountManager frm = new AccountManager();
            frm.Show(this);
            frm.LoadAcc();
        }

        private void btnTableShow_Click(object sender, EventArgs e)
        {
            TableForm frm = new TableForm();
            frm.Show(this);
            frm.LoadTable();
        }
    }
}
