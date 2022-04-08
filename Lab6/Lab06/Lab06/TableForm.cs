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
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();
        }
        public void LoadTable()
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            string query = "SELECT * FROM [TABLE]";

            sqlConnection.Open();
            sqlCommand.CommandText = query;
            sqlCommand.ExecuteNonQuery();
            this.Text = "Danh sách tất cả các bàn";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable("Table");
            adapter.Fill(dt);

            dgvTable.DataSource = dt;
            dgvTable.Columns[0].ReadOnly = true;

            sqlConnection.Close();
            adapter.Dispose();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text)) return false;
            else if (string.IsNullOrEmpty(cbbStatus.Text)) return false;
            else if (string.IsNullOrEmpty(txtCapacity.Text)) return false;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand command = sqlConnection.CreateCommand();

                sqlConnection.Open();

                command.CommandText = "SELECT Name FROM [Table] WHERE Name = '" + txtName.Text + "'";
                var check = command.ExecuteScalar();

                if (check == null)
                {
                    command.CommandText = string.Format("insert into [Table](Name, Status, Capacity) values (N'{0}', {1}, {2})",
                        txtName.Text,cbbStatus.Text == "Trống" ? "0":"1", txtCapacity.Text);

                    int numOfRows = command.ExecuteNonQuery();
                    if (numOfRows == 1)
                    {
                        LoadTable();
                        ResetForm();
                        MessageBox.Show("Thêm bàn mới thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Bàn đã tồn tại, xin vui lòng thử lại");
                }
                sqlConnection.Close();
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private void ResetForm()
        {
            txtName.Text = "";
            cbbStatus.Text = "";
            txtCapacity.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validation()) {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                string id = dgvTable.SelectedRows[0].Cells[0].Value.ToString();
                sqlConnection.Open();

                sqlCommand.CommandText = "SELECT Name FROM [Table] WHERE Name = '" + txtName.Text + "'";
                var check = sqlCommand.ExecuteScalar();

                if (check == null) {

                    sqlCommand.CommandText = string.Format("UPDATE [Table] SET Name = N'{0}', Status = {1}, Capacity = {2} WHERE ID = {3} ",
                        txtName.Text, cbbStatus.Text == "Trống" ? "0" : "1", txtCapacity.Text, id);


                    int numOfRows = sqlCommand.ExecuteNonQuery();

                    if (numOfRows == 1)
                    {
                        LoadTable();
                        ResetForm();
                        MessageBox.Show("Cập nhật bàn thành công");
                    }
                    else
                    {
                        MessageBox.Show("Loi", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bàn đã tồn tại xin vui lòng thử lại");
                }
                sqlConnection.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var rowSelect = dgvTable.SelectedRows[0];
            string id = dgvTable.SelectedRows[0].Cells[0].Value.ToString();

            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            string query = string.Format("Delete from Bills Where TableID = {0}", id);
            sqlCommand.CommandText = query;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "Delete from [Table] WHERE ID = " + id;
            sqlCommand.ExecuteNonQuery();

            dgvTable.Rows.Remove(rowSelect);
            LoadTable();
            ResetForm();
            MessageBox.Show("Đã xóa thành công");

            sqlConnection.Close();
        }

        private void dgvTable_Click(object sender, EventArgs e)
        {
            int index = dgvTable.CurrentRow.Index;

            txtName.Text = dgvTable.Rows[index].Cells["Name"].Value.ToString();
            cbbStatus.Text = dgvTable.Rows[index].Cells["Status"].Value.ToString() == "0" ? "Trống":"Có người";
            txtCapacity.Text = dgvTable.Rows[index].Cells["Capacity"].Value.ToString();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count > 0)
                btnDelete.PerformClick();
        }

        private void tsmShowBills_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var rowSelect = dgvTable.SelectedRows[0];
            string id = dgvTable.SelectedRows[0].Cells[0].Value.ToString();

            FrmTableBills frm = new FrmTableBills();
            frm.Show(this);
            frm.LoadBills(id);
        }

        private void showBillsMemory_Click(object sender, EventArgs e)
        {
            FrmBoughtMemory frm = new FrmBoughtMemory();
            frm.Show(this);
            frm.LoadMemory();
        }
    }
}
