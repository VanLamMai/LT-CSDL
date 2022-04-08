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
    public partial class OrdersForm : Form
    {
       
        public OrdersForm()
        {
            InitializeComponent();
            this.Load_Bills();
        }
        private void Load_Bills()
        {

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                this.Text = "Danh sách hóa đơn";
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlConnection.Open();

                sqlCommand.CommandText = "EXECUTE Bills_GetByDate @date";
                
                sqlCommand.Parameters.Add("@date", SqlDbType.SmallDateTime);
                
                sqlCommand.Parameters["@date"].Value = dtpDate.Value.ToShortDateString();

                sqlCommand.ExecuteNonQuery();


                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable("Bills");
                adapter.Fill(table);

                dvgBills.DataSource = table;
                dvgBills.Columns[0].ReadOnly = true;
                
                sqlCommand.CommandText = "Select SUM(Amount) from Bills where CheckoutDate = @date";
                var doanhThu = sqlCommand.ExecuteScalar();


                sqlConnection.Close();
                sqlCommand.Dispose();
                setBillsToG(table);
                setSize(table);


                
                lbl_Date.Text = dtpDate.Value.ToShortDateString();
                lblTotal.Text = doanhThu.ToString();

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
        void setBillsToG(DataTable table)
        {
            
            dvgBills.Columns[0].ReadOnly = true;
            dvgBills.Columns[0].HeaderCell.Value = "Mã hóa đơn thanh toán";
            dvgBills.Columns[1].HeaderCell.Value = "Tên hóa đơn thanh toán";
            dvgBills.Columns[2].HeaderCell.Value = "Mã bàn";
            dvgBills.Columns[3].HeaderCell.Value = "Số tiền";
            dvgBills.Columns[4].HeaderCell.Value = "Giảm gá";
            dvgBills.Columns[5].HeaderCell.Value = "Thuế";
            dvgBills.Columns[6].HeaderCell.Value = "Trạng thái";
            dvgBills.Columns[7].HeaderCell.Value = "Ngày lập";
            dvgBills.Columns[8].HeaderCell.Value = "Tài khoản lập";
            //
           
        }
        void setSize(DataTable table)
        {
            dvgBills.DataSource = table;
            dvgBills.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgBills.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dvgBills_DoubleClick(object sender, EventArgs e)
        {
            OrderDetailsForm ordersDetailsForm = new OrderDetailsForm();
            string billID = dvgBills.SelectedRows[0].Cells[0].Value.ToString();
            ordersDetailsForm.Load_Details(billID);
            ordersDetailsForm.Show();
            
        }
    }
}
