using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                        // 😏 Maybe use a like operation instead of strict equivalence?
                        query = $"select * from tb_ward where ward_id = {searchValue}";
                    }
                    else
                    {
                        // 😏 Also here, prefer like, though expensive, removes strict equality
                        query = $"select * from tb_ward where ward_name = '{searchValue}'";
                    }
                }
                Console.WriteLine($"Query {query}");
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
    }
}

