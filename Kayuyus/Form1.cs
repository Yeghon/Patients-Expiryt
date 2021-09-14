using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kayuyus
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        string connectionString = ("Server=mc-ict-53;Database=expiry_registration;User=sa;Password=20304wW.;");
        string searchValue = "";
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select top 5 * from tb_ward";
                if (searchValue != "")
                {
                    // check if number
                    if (Regex.IsMatch(searchValue.Trim(), @"^\d+$"))
                    {
                        query = $"select * from tb_ward where ward_id like {searchValue}";
                    }
                    else
                    {
                        query = $"select * from tb_ward where ward_name like '%{searchValue}%'";
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Failed");
            }
            finally
            {
                connection.Close();
            }

        }

        private void tbwardName_TextChanged(object sender, EventArgs e)
        {
            searchValue = tbwardName.Text;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lstCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txt = lstCity.GetItemText(lstCity.SelectedItem);
            MessageBox.Show(txt);
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
        }

        private void progress_Click(object sender, EventArgs e)
        {
            /*SqlCommand command = null;
            SqlDataReader dataReader = null;
            try
            {
                connection.Open();
                string sql = "SELECT * FROM tb_ward tb", output = "";
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    output = $"{output} {dataReader.GetValue(0)} -- {dataReader.GetValue(1)} -- {dataReader.GetValue(2)}\n";
                }
                MessageBox.Show(output);
            } finally {
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }*/
            connection.Open();
            var TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            string sql = $"INSERT tb_ward VALUES (11, 'Kayuyu', {TimeStamp}), (12, 'AKUH', {TimeStamp})";
            Console.WriteLine(sql);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.InsertCommand = new SqlCommand(sql, connection); 
            dataAdapter.InsertCommand.ExecuteNonQuery();
            //command.Dispose();
            connection.Close();
        }
    }
}

