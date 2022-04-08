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
    public partial class AddCategoryForm : Form
    {
        public AddCategoryForm()
        {
            InitializeComponent();
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=DESKTOP-EQOPDBI\\SQLEXPRESS; database = RestaurantManagement; Integrated Security = true; ";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "EXECUTE InsertCategory @id OUTPUT,@name, @type";
                
                sqlCommand.Parameters.Add("@id", SqlDbType.Int);
                sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 1000);
                sqlCommand.Parameters.Add("@type", SqlDbType.NVarChar, 100);

                sqlCommand.Parameters["@id"].Direction = ParameterDirection.Output;
               
                sqlCommand.Parameters["@name"].Value = txtCategoryName.Text;
                sqlCommand.Parameters["@type"].Value = txtType.Text;

                sqlConnection.Open();

                int numRowAffected = sqlCommand.ExecuteNonQuery();
                
                if (numRowAffected > 0)
                {
                    string categoryID = sqlCommand.Parameters["@id"].Value.ToString();

                    MessageBox.Show("Thêm thành công nhóm món ăn",  "Message");

                    this.ResetText();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm nhóm món ăn không thành công");
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
    }
}
