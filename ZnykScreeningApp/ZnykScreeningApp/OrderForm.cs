using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devart.Data;
using Devart.Data.Bigcommerce;
using Devart.Data.SqlShim;
using System.Windows.Forms;

namespace ZnykScreeningApp
{
    public partial class OrderForm : Form
    {
        BigcommerceConnectionStringBuilder connectionStringBuilder = new BigcommerceConnectionStringBuilder();
        string filterOne;
        string filterTwo;

        public OrderForm()
        {
            InitializeComponent();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sortButton.Enabled = true;
            filterOne = comboBox1.Text;
            textBox1.Enabled = true;
            if (filterOne == "All Products")
            {
                textBox1.Clear();
                textBox1.Enabled = false;
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            //resets the grid
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear(); ;
            dataGrid.Refresh();

            filterTwo = textBox1.Text;

            const string connectionString = "server=https://store-h2g0srxog6.mybigcommerce.com/api/v2/; userid=Tester; Authentication Token=120478d1c1bcf0f8b69a3beeadbe21cb7dcdd90c;";
            //const string sql = ("SELECT " + filterOne + ", " + filterTwo + "FROM Products");

            using (BigcommerceConnection connection = new BigcommerceConnection(connectionString))
            {
                connection.Open();
                using (BigcommerceCommand command = connection.CreateCommand())
                {
                    switch (filterOne)
                    {
                        case ("All Products"):
                            {
                                command.CommandText = ("SELECT Name, Price, sku FROM Products");
                            }
                            break;

                        case ("Search by Keyword"):
                            {
                                command.CommandText = ("SELECT Name, Price, sku FROM Products WHERE Name LIKE '%" + filterTwo + "%'");
                            }
                            break;

                        case ("Search by Price"):
                            {
                                command.CommandText = ("SELECT Name, Price, sku FROM Products WHERE Price = " + filterTwo);
                            }
                            break;

                        case ("Search by SKU"):
                            {
                                command.CommandText = ("SELECT Name, Price, sku FROM Products WHERE SKU = '" + filterTwo + "'");
                            }
                            break;
                    }

                    using (BigcommerceDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("{0}\t{1}", reader.GetValue(0), reader.GetValue(1));
                            dataGrid.ColumnCount = 3;
                            dataGrid.Columns[0].Name = "Product Name";
                            dataGrid.Columns[1].Name = "Price";
                            dataGrid.Columns[2].Name = "Product SKU";

                            //Add data to columns
                            string[] row = new string[] { reader.GetValue(0).ToString().Substring(9), reader.GetValue(1).ToString(), reader.GetValue(2).ToString() };
                            dataGrid.Rows.Add(row);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
