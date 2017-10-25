using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Devart.Data;
using Devart.Common;
using Devart.Data.Bigcommerce;
using Devart.Data.SqlShim;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ZnykScreeningApp
{
    public partial class LoginForm : Form
    {
        BigcommerceConnectionStringBuilder connectionStringBuilder = new BigcommerceConnectionStringBuilder();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string UName;

            UName = UsernameBox.Text;           

            //Connect to BigCommerce server
            connectionStringBuilder.Server = "https://store-h2g0srxog6.mybigcommerce.com/api/v2/";
            connectionStringBuilder.UserId = UName;
            connectionStringBuilder.AuthenticationToken = "120478d1c1bcf0f8b69a3beeadbe21cb7dcdd90c";

            BigcommerceConnection bigcommerceConnection = new BigcommerceConnection(connectionStringBuilder.ConnectionString);

            bigcommerceConnection.Open();
            //MessageBox.Show(bigcommerceConnection.ServerVersion, "Hello!");
            this.Visible = false;

            OrderForm orderForm = new OrderForm();
            orderForm.Show();

            bigcommerceConnection.Close();
        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
