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
    public partial class Form1 : Form
    {
        private DataTable foodTable;
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadCategory()
        {
            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, Name FROM Category";

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
           
            sqlConnection.Open();
           
            adapter.Fill(dt);

            sqlConnection.Close();
            sqlConnection.Dispose();
        
            cbbCategory.DataSource = dt;
           
            cbbCategory.DisplayMember = "Name";
    
            cbbCategory.ValueMember = "ID";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadCategory();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedIndex == -1) return;

            string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT * FROM Food WHERE FoodCategoryID = @categoryId";

            sqlCommand.Parameters.Add("@categoryId", SqlDbType.Int);

            if(cbbCategory.SelectedValue is DataRowView)
            {
                DataRowView rowView = cbbCategory.SelectedValue as DataRowView;
                sqlCommand.Parameters["@categoryId"].Value = rowView["ID"];
            }
            else
            {
                sqlCommand.Parameters["@categoryId"].Value = cbbCategory.SelectedValue;
            }
            sqlConnection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            foodTable = new DataTable();

            adapter.Fill(foodTable);

            sqlConnection.Close();
            sqlConnection.Dispose();

            dgvFoodList.DataSource = foodTable;

            lblQuantity.Text = foodTable.Rows.Count.ToString();
            lblCatName.Text = cbbCategory.Text;
            setFoodToG(foodTable);
            setSize(foodTable);
        }

        private void tsmCalculateQuantity_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-M1B6BBF\SQLEXPRESS;Initial Catalog=RestaurantManagement;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT @numSaleFood = sum(Quantity) " +
                "FROM BillDetails " +
                "WHERE FoodID = @foodId";

            if(dgvFoodList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];

                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                sqlCommand.Parameters.Add("@foodId", SqlDbType.Int);
                sqlCommand.Parameters["@foodId"].Value = rowView["ID"];

                sqlCommand.Parameters.Add("@numSaleFood", SqlDbType.Int);
                sqlCommand.Parameters["@numSaleFood"].Direction = ParameterDirection.Output;

                sqlConnection.Open();
             
                sqlCommand.ExecuteNonQuery();

                string result = sqlCommand.Parameters["@numSaleFood"].Value.ToString();
                MessageBox.Show("Tổng số lượng món " + rowView["Name"] + " Đã bán:" + result + " " + rowView["Unit"]);

                sqlConnection.Close();
            }
            sqlCommand.Dispose();
            sqlConnection.Dispose();


        }
        void setFoodToG(DataTable table)
        {
            dgvFoodList.DataSource = table;
            dgvFoodList.Columns[0].ReadOnly = true;
            dgvFoodList.Columns[0].HeaderCell.Value = "Mã món ăn";
            dgvFoodList.Columns[1].HeaderCell.Value = "Tên món ăn";
            dgvFoodList.Columns[2].HeaderCell.Value = "Đơn vị";
            dgvFoodList.Columns[3].HeaderCell.Value = "Nhóm món ăn";
            dgvFoodList.Columns[4].HeaderCell.Value = "Giá";
            dgvFoodList.Columns[5].HeaderCell.Value = "Ghi chú";
            dgvFoodList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        void setSize(DataTable table)
        {
            dgvFoodList.DataSource = table;
            dgvFoodList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFoodList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFoodList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFoodList.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFoodList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFoodList.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void tsmAddFood_Click(object sender, EventArgs e)
        {
            FoodInfoForm foodForm = new FoodInfoForm();
            foodForm.FormClosed += new FormClosedEventHandler(foodForm_FormClosed);
            foodForm.Show(this);

        }
        void foodForm_FormClosed(object sender,FormClosedEventArgs e)
        {
            int index = cbbCategory.SelectedIndex;
            cbbCategory.SelectedIndex = -1;
            cbbCategory.SelectedIndex = index;
        }

        private void tsmUpdateFood_Click(object sender, EventArgs e)
        {
            if(dgvFoodList.SelectedRows.Count >0)
            {
                DataGridViewRow selectedRow = dgvFoodList.SelectedRows[0];
                DataRowView rowView = selectedRow.DataBoundItem as DataRowView;

                FoodInfoForm foodForm = new FoodInfoForm();
                foodForm.FormClosed += new FormClosedEventHandler(foodForm_FormClosed);
                foodForm.Show(this);
                foodForm.DisplayFoodInfo(rowView);
            }
        }

        private void txtSearchByName_TextChanged(object sender, EventArgs e)
        {
            if (foodTable == null) return;
        
            string filterExpression = " Name like '%" + txtSearchByName.Text + "%'";
            string sortExpression = "Price DESC";
            DataViewRowState rowStateFilter = DataViewRowState.OriginalRows;

            DataView foodView = new DataView(foodTable, filterExpression, sortExpression, rowStateFilter);

            dgvFoodList.DataSource = foodView;
        }

        private void oderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdersForm ordersForm = new OrdersForm();
            ordersForm.Show(this);
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountForm frm = new AccountForm();
            frm.Show(this);
        }
    }
}
