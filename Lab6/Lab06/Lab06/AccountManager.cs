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
    public partial class AccountManager : Form
    {
        public AccountManager()
        {
            InitializeComponent();
        }

        public void LoadAcc()
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Account";

            connection.Open();

            string categoryName = command.ExecuteScalar().ToString();
            this.Text = "Danh sách toàn bộ tài khoản";

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable("Food");
            adapter.Fill(table);

            dgvAccount.DataSource = table;

            // Prevent user to edit ID
            dgvAccount.Columns[0].ReadOnly = true;

            connection.Close();
        }

        private void cbbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();

            if (cbbSapXep.SelectedItem == "Nhóm")
            {
                this.dgvAccount.DataSource = null;
                command.CommandText = "SELECT RoleID, Account.AccountName, Password, FullName, Email, Tell, DateCreated FROM Account, RoleAccount " +
                    " WHERE Account.AccountName = RoleAccount.AccountName" +
                    " ORDER BY RoleAccount.RoleID";

                connection.Open();

                this.Text = "Danh sách toàn bộ tài khoản";

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable("Food");
                adapter.Fill(table);

                dgvAccount.DataSource = table;

                // Prevent user to edit ID
                dgvAccount.Columns[0].ReadOnly = true;

                connection.Close();
            }
            else
            {
                this.dgvAccount.DataSource = null;
                command.CommandText = "SELECT Actived, Account.AccountName, Password, FullName, Email, Tell, DateCreated FROM Account, RoleAccount " +
                    " WHERE Account.AccountName = RoleAccount.AccountName" +
                    " ORDER BY RoleAccount.Actived";

                connection.Open();

                this.Text = "Danh sách toàn bộ tài khoản";

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable("Food");
                adapter.Fill(table);

                dgvAccount.DataSource = table;

                // Prevent user to edit ID
                dgvAccount.Columns[0].ReadOnly = true;

                connection.Close();
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection= new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlConnection.Open();

                sqlCommand.CommandText = "SELECT * FROM Account WHERE Account.AccountName = '" + txtAccount.Text+"'";
                var check = sqlCommand.ExecuteScalar();

                if (check == null)
                {
                    sqlCommand.CommandText = string.Format("insert into Account(AccountName,Password, FullName, Email, Tell, DateCreated) values ('{0}', '{1}', N'{2}', '{3}', '{4}', '{5}')",
                        txtAccount.Text, txtPass.Text, txtName.Text, txtEmail.Text, txtTell.Text, DateTime.Now);
                    int numOfRows = sqlCommand.ExecuteNonQuery();
                    if (numOfRows == 1)
                    {
                        LoadAcc();
                        ResetForm();
                        MessageBox.Show("Thêm tài khoản mới thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản đã tồn tại, xin thử lại");
                }
                sqlConnection.Close();
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi",MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtAccount.Text)) return false;
            else if (string.IsNullOrEmpty(txtPass.Text)) return false;
            else if (string.IsNullOrEmpty(txtName.Text)) return false;
            return true;
        }

        private void dgvAccount_Click(object sender, EventArgs e)
        {
            int index =  dgvAccount.CurrentRow.Index;

            txtAccount.Text = dgvAccount.Rows[index].Cells["AccountName"].Value.ToString();
            txtPass.Text = dgvAccount.Rows[index].Cells["Password"].Value.ToString();
            txtName.Text = dgvAccount.Rows[index].Cells["FullName"].Value.ToString();
            txtEmail.Text = dgvAccount.Rows[index].Cells["Email"].Value.ToString();
            txtTell.Text = dgvAccount.Rows[index].Cells["Tell"].Value.ToString();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConn = new SqlConnection(connectionString);
                SqlCommand sqlComd = sqlConn.CreateCommand();

                sqlComd.CommandText = string.Format("UPDATE Account SET AccountName = '{0}', Password = '{1}', FullName = N'{2}', Email = '{3}', Tell = '{4}', DateCreated = '{5}' WHERE AccountName = '{0}' ",
                    txtAccount.Text, txtPass.Text, txtName.Text, txtEmail.Text, txtTell.Text, DateTime.Now);

                sqlConn.Open();

                int numOfRows = sqlComd.ExecuteNonQuery();

                if (numOfRows == 1)
                {
                    LoadAcc();
                    ResetForm();
                    MessageBox.Show("Cập nhật nhóm món ăn thành công");
                }
                else
                {
                    MessageBox.Show("Loi", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                sqlConn.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
        }
        private void ResetForm()
        {
            txtAccount.Text = "";
            txtPass.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtTell.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string defaultPass = "123456";
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlComd = sqlConnection.CreateCommand();

            sqlComd.CommandText = string.Format("UPDATE Account SET Password = '{1}' WHERE AccountName = '{0}' ",
                txtAccount.Text, defaultPass);

            sqlConnection.Open();

            sqlComd.ExecuteNonQuery();
            MessageBox.Show("Cập nhật thành công");
            LoadAcc();
            ResetForm();
            sqlConnection.Close();
        }

        private void ttsmDeleteAcc_Click(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count == 0) return;
            var rowSelect = dgvAccount.SelectedRows[0];
            string account = rowSelect.Cells[0].Value.ToString();

            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConn = new SqlConnection(connectionString);
            SqlCommand sqlComd = sqlConn.CreateCommand();

            string query = string.Format("UPDATE RoleAccount SET Actived = '0' WHERE AccountName = '{0}'",account);
            sqlComd.CommandText = query;
            sqlConn.Open();

            int numOfRowsEffected = sqlComd.ExecuteNonQuery();
            if (numOfRowsEffected == 1)
            {
                dgvAccount.Rows.Remove(rowSelect);
                MessageBox.Show("Đã xóa thành công");
            }

            sqlConn.Close();
        }

        private void tsmDeleteRole_Click(object sender, EventArgs e)
        {
            if (dgvAccount.SelectedRows.Count == 0) return;
            var rowSelect = dgvAccount.SelectedRows[0];
            string account = rowSelect.Cells[0].Value.ToString();

            RoleAcc frm = new RoleAcc();
            frm.Show(this);
            frm.LoadRole(account);
        }
    }
}
