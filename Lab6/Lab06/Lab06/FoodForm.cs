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
    public partial class frmFood : Form
    {
        int categoryID;
        public frmFood()
        {
            InitializeComponent();
        }
        public void LoadFood(int categoryID)
        {
            this.categoryID = categoryID;
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlComd = sqlConnection.CreateCommand();

            sqlComd.CommandText = "SELECT Name FROM Category WHERE ID = " + categoryID;

            sqlConnection.Open();

            string catName = sqlComd.ExecuteScalar().ToString();
            this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;

            sqlComd.CommandText = "SELECT * FROM Food WHERE FoodCategoryID = " + categoryID;

            SqlDataAdapter da = new SqlDataAdapter(sqlComd);
            DataTable dt = new DataTable("Food");
            da.Fill(dt);

            dgvFood.DataSource = dt;

            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlConnection.Open();

            for (int i = 0; i< dgvFood.Rows.Count - 1; i++)
            {
                int id = (int)dgvFood.Rows[i].Cells["ID"].Value;
                sqlCommand.CommandText = "SELECT * FROM Food WHERE ID = " + id;
                var checkID = sqlCommand.ExecuteScalar();

                if(checkID == null)
                {
                    string query = string.Format(" INSERT INTO Food(Name, Unit, FoodCategoryID, Price, Notes) " +
                    "VALUES (N'{0}', N'{1}', {2}, {3}, N'{4}')",
                    dgvFood.Rows[i].Cells["Name"].Value,
                    dgvFood.Rows[i].Cells["Unit"].Value,
                    categoryID,
                    dgvFood.Rows[i].Cells["Price"].Value,
                    dgvFood.Rows[i].Cells["Notes"].Value.ToString());
                    sqlCommand.CommandText = query;
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Thêm mới thành công");
                }
                else
                {
                    string query = string.Format(" UPDATE Food SET Name = N'{0}', Unit = N'{1}', FoodCategoryID = {2}, Price = {3}, Notes = N'{4}' WHERE ID = {5}",
                    dgvFood.Rows[i].Cells["Name"].Value,
                    dgvFood.Rows[i].Cells["Unit"].Value,
                    categoryID,
                    dgvFood.Rows[i].Cells["Price"].Value,
                    dgvFood.Rows[i].Cells["Notes"].Value.ToString(), 
                    id.ToString());
                    sqlCommand.CommandText = query;
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công");
                }
            }

            sqlConnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count == 0) return;
            var rowSelect = dgvFood.SelectedRows[0];
            string foodID = rowSelect.Cells[0].Value.ToString();

            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            string query = "DELETE FROM Food WHERE ID = " + foodID;

            sqlConnection.Open();
            sqlCommand.CommandText = "DELETE FROM BillDetails WHERE FoodID = " + foodID;
            sqlCommand.ExecuteNonQuery();
               
            sqlCommand.CommandText = query;
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            if(numOfRowsEffected == 1)
            {
                dgvFood.Rows.Remove(rowSelect);
                MessageBox.Show("Đã xóa thành công");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi");
                return;
            }

            sqlConnection.Close();
        }
    }
}
