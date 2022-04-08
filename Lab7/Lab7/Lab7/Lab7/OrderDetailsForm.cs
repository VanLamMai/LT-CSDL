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
    public partial class OrderDetailsForm : Form
    {
        
        public OrderDetailsForm()
        {
            InitializeComponent();
            
        }
        public void Load_Details(string billID)
        {
            try
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlConnection.Open();

                sqlCommand.CommandText = "EXECUTE BillDetails_GetByID @id";
                
                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                
                sqlCommand.Parameters["@id"].Value = billID; 

                sqlCommand.ExecuteNonQuery();


                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable table = new DataTable("Bills Details");
                adapter.Fill(table);

                dvgOrderDtails.DataSource = table;
                dvgOrderDtails.Columns[0].ReadOnly = true;
                setSize(table);
                setBillsDetailsToG(table);

                sqlConnection.Close();
                sqlCommand.Dispose();
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
        void setBillsDetailsToG(DataTable table)
        {
            dvgOrderDtails.DataSource = table;
            dvgOrderDtails.Columns[0].ReadOnly = true;
            dvgOrderDtails.Columns[0].HeaderCell.Value = "Mã hóa đơn thanh toán";
            dvgOrderDtails.Columns[1].HeaderCell.Value = "Mã hóa đơn";
            dvgOrderDtails.Columns[2].HeaderCell.Value = "Mã món ăn";
            dvgOrderDtails.Columns[3].HeaderCell.Value = "Số Lượng";
            
            
        }
        void setSize(DataTable table)
        {
            dvgOrderDtails.DataSource = table;
            dvgOrderDtails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgOrderDtails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgOrderDtails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvgOrderDtails.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           

        }
    }
}
