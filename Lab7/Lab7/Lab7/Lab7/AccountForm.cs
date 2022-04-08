using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
        }
        private void Load_Account()
        {
            this.Text = "Danh sách tài khoản";
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT *  FROM Account";

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            //Mở kết nối
            sqlConnection.Open();
            //Lấy dữ liệu từ csdl đưa vào DataTable
            adapter.Fill(dt);
            dgvAccount.DataSource = dt;
            setAccountToG(dt);
            setSize(dt);
            //Đóng kết nối và giải phóng bộ nhớ
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
        void setAccountToG(DataTable table)
        {
            dgvAccount.DataSource = table;
            dgvAccount.Columns[0].ReadOnly = true;
            dgvAccount.Columns[0].HeaderCell.Value = "Tên tài khoản";
            dgvAccount.Columns[1].Visible = false;
            dgvAccount.Columns[2].HeaderCell.Value = "Họ và Tên";
            dgvAccount.Columns[3].HeaderCell.Value = "Email";
            dgvAccount.Columns[4].HeaderCell.Value = "Số điện thoại";
            dgvAccount.Columns[5].HeaderCell.Value = "Ngày tạo";
            dgvAccount.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        void setSize(DataTable table)
        {
            dgvAccount.DataSource = table;
            dgvAccount.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAccount.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAccount.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAccount.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAccount.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAccount.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            this.Load_Account();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "EXECUTE InsertAccount @AccountName,@Password,@Fullname,@Email,@Tell,@DateCreated";

                //Thêm tham số vào đối tượng Command
                sqlCommand.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100);
                sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 200);
                sqlCommand.Parameters.Add("@Fullname", SqlDbType.NVarChar, 1000);
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 1000);
                sqlCommand.Parameters.Add("@Tell", SqlDbType.NVarChar, 200);
                sqlCommand.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime);

                //sqlCommand.Parameters["@AccountName"].Direction = ParameterDirection.Output;
                ////Truyền giá trị vào thủ tục qua tham số
                sqlCommand.Parameters["@AccountName"].Value = txtNameAccount.Text;
                sqlCommand.Parameters["@Password"].Value = txtPass.Text;
                sqlCommand.Parameters["@Fullname"].Value = txtName.Text;
                sqlCommand.Parameters["@Email"].Value = txtEmail.Text;
                sqlCommand.Parameters["@Tell"].Value = mtxtSDT.Text;
                sqlCommand.Parameters["@DateCreated"].Value = DateTime.Now.ToShortDateString();

                sqlConnection.Open();

                int numRowAffected = sqlCommand.ExecuteNonQuery();
                
                if (numRowAffected > 0)
                { 
                    MessageBox.Show("Thêm thành công tài khoản", "Message");

                    this.ResetForm();
                    this.Load_Account();
                    
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản không thành công","Message");
                }


            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message, "SQL Error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "EXECUTE Account_Update @AccountName ,@Password,@Fullname,@Email,@Tell,@DateCreated";

                sqlCommand.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100);
                sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 200);
                sqlCommand.Parameters.Add("@Fullname", SqlDbType.NVarChar, 1000);
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 1000);
                sqlCommand.Parameters.Add("@Tell", SqlDbType.NVarChar, 200);
                sqlCommand.Parameters.Add("@DateCreated", SqlDbType.SmallDateTime);

                sqlCommand.Parameters["@AccountName"].Value = txtNameAccount.Text;
                sqlCommand.Parameters["@Password"].Value = txtPass.Text;
                sqlCommand.Parameters["@Fullname"].Value = txtName.Text;
                sqlCommand.Parameters["@Email"].Value = txtEmail.Text;
                sqlCommand.Parameters["@Tell"].Value = mtxtSDT.Text;
                sqlCommand.Parameters["@DateCreated"].Value = DateTime.Now.ToShortDateString();

                sqlConnection.Open();

                int numRowAffected = sqlCommand.ExecuteNonQuery();

                if (numRowAffected > 0)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công", "Message");
                    this.ResetForm();
                    this.Load_Account();
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản không thành công");
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message, "SQL Error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }

        }

        private void dgvAccount_Click(object sender, EventArgs e)
        {
            int index = dgvAccount.CurrentRow.Index;
            if (index != null)
            {
                txtNameAccount.Text = dgvAccount.Rows[index].Cells["AccountName"].Value.ToString();
                txtPass.Text = dgvAccount.Rows[index].Cells["Password"].Value.ToString();
                txtName.Text = dgvAccount.Rows[index].Cells["FullName"].Value.ToString();
                txtEmail.Text = dgvAccount.Rows[index].Cells["Email"].Value.ToString();
                mtxtSDT.Text = dgvAccount.Rows[index].Cells["Tell"].Value.ToString();
                dtpDate.Text = dgvAccount.Rows[index].Cells["DateCreated"].Value.ToString();

                btnUpdate.Enabled = true;

            }
        }
        private void ResetForm()
        {
            txtNameAccount.Text = "";
            txtPass.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            mtxtSDT.Text = "";
            btnUpdate.Enabled = false;
        }

        private void tmsRole_Click(object sender, EventArgs e)
        {

        }

        private void tsmDiary_Click(object sender, EventArgs e)
        {

        }
    }

}
